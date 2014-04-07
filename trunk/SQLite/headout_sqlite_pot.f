      subroutine headout_sqlite_pot

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table pot in SQLite database for potholes
!!     The file handle for output.pot is 125

      use parm

      integer :: j,potbasiccolnum,potvaluecolnum

      !!create table pot
      if (iwtr == 1) then
          tblpot = 'pot'

          !!delete any existing table
          call sqlite3_delete_table( db, tblpot)

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
          call sqlite3_create_table( db, tblpot, colpot )
          call headout_sqlite_createindex("pot_index",tblpot,"SUB,HRU")
      end if
      end
