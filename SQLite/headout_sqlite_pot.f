      subroutine headout_sqlite_pot

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table pot in SQLite database for potholes
!!     The file handle for output.pot is 125

      use parm

      integer :: j,potbasiccolnum,potvaluecolnum
      character(len=14) :: indexname

      !!allocate table names for all hrus
      allocate(tblpot(nhru))
      do j = 1, nhru
        write(tblpot(j),5000) j
        write(*,*) tblpot(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblpot(j))
      end do

      !!create table pot
      if (iwtr == 1) then
          potvaluecolnum = size(hedpot)
          potbasiccolnum = 1 + datecol_num

          allocate( colpot(potbasiccolnum + potvaluecolnum) )

          call sqlite3_column_props( colpot(1), "HRU", SQLITE_INT)
          call headout_sqlite_adddate(colpot,
     &     potbasiccolnum + potvaluecolnum,2)
          do j = 1, potvaluecolnum
             call sqlite3_column_props(colpot(potbasiccolnum+j),
     &                                          hedpot(j),SQLITE_REAL)
          end do

          do j = 1, nhru
            call sqlite3_create_table( db, tblpot(j), colpot )

            write(indexname,5001) j
            write(*,*) indexname
            call headout_sqlite_createindex(indexname,tblpot(j),"",1)
          end do
      end if
      return

5000  format ('pot',i5.5)
5001  format ('pot',i5.5,'_index')

      end
