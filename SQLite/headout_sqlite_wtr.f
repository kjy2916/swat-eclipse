      subroutine headout_sqlite_wtr

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table wtr in SQLite database for depressin area
!!     The file handle for output.wtr is 29

      use parm

      integer :: j,wtrbasiccolnum,wtrvaluecolnum
      character(len=14) :: indexname

      !!allocate table names for all hrus
      allocate(tblwtr(nhru))
      do j = 1, nhru
        write(tblwtr(j),5000) j
        write(*,*) tblwtr(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblwtr(j))
      end do

      !!create table rsv
      if (iwtr == 1) then
          wtrbasiccolnum = 2 + datecol_num
          wtrvaluecolnum = 40

          allocate( colwtr(wtrbasiccolnum + wtrvaluecolnum) )

          call sqlite3_column_props( colwtr(1), "LULC", SQLITE_CHAR,4)
          call sqlite3_column_props( colwtr(2), "MGT", SQLITE_INT)
          call headout_sqlite_adddate(colwtr,
     &                  wtrbasiccolnum + wtrvaluecolnum,3)

          do j = 1, wtrvaluecolnum
             call sqlite3_column_props(colwtr(wtrbasiccolnum+j),
     &                                          hedwtr(j),SQLITE_REAL)
          end do

          do j = 1, nhru
            call sqlite3_create_table( db, tblwtr(j), colwtr )

            write(indexname,5001) j
            write(*,*) indexname
            call headout_sqlite_createindex(indexname,tblwtr(j),"",1)
          end do
      end if
      return

5000  format ('wtr',i5.5)
5001  format ('wtr',i5.5,'_index')

      end
