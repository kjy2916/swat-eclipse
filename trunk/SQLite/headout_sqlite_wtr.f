      subroutine headout_sqlite_wtr

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table wtr in SQLite database for depressin area
!!     The file handle for output.wtr is 29

      use parm

      integer :: j,wtrbasiccolnum,wtrvaluecolnum

      !!create table rsv
      if (iwtr == 1) then
          tblwtr = 'wtr'
          wtrbasiccolnum = 4 + datecol_num
          wtrvaluecolnum = 40

          allocate( colwtr(wtrbasiccolnum + wtrvaluecolnum) )

          call sqlite3_column_props( colwtr(1), "SUB", SQLITE_INT)
          call sqlite3_column_props( colwtr(2), "HRU", SQLITE_INT)
          call sqlite3_column_props( colwtr(3), "LULC", SQLITE_CHAR,4)
          call sqlite3_column_props( colwtr(4), "MGT", SQLITE_INT)
          call headout_sqlite_adddate(colwtr,
     &                  wtrbasiccolnum + wtrvaluecolnum,5)

          do j = 1, wtrvaluecolnum
             call sqlite3_column_props(colwtr(wtrbasiccolnum+j),
     &                                          hedwtr(j),SQLITE_REAL)
          end do
          call sqlite3_delete_table( db, tblwtr)
          call sqlite3_create_table( db, tblwtr, colwtr )
          call headout_sqlite_createindex("wtr_index",tblwtr,"HRU,SUB")
      end if


      end
