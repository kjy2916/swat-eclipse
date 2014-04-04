      subroutine headout_sqlite_snu

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create three tables in SQLite database for management data
!!     which is previously in output.snu (output.sol)
!!     The file handle for output.snu is 121

      use parm

      integer :: j,basiccolnum,valuecolnum

      tblsnu = 'snu'

      call sqlite3_delete_table( db, tblsnu)

      if(imgt == 1) then
          valuecolnum = size(hedsnu)
          basiccolnum = 5
          allocate( colsnu(basiccolnum + valuecolnum + 2) )
          call sqlite3_column_props(colsnu(1),"SUB",SQLITE_INT)
          call sqlite3_column_props(colsnu(2),"HRU",SQLITE_INT)
          call sqlite3_column_props(colsnu(3),"YR",SQLITE_INT)
          call sqlite3_column_props(colsnu(4),"MO",SQLITE_INT)
          call sqlite3_column_props(colsnu(5),"DA",SQLITE_INT)
          do j=1,valuecolnum
            call sqlite3_column_props( colsnu(basiccolnum + j),
     &                                          hedsnu(j), SQLITE_REAL)
          end do

          call sqlite3_create_table( db, tblsnu, colsnu )
          call sqlite3_create_index( db, "snu_index",tblsnu,
     &                                              "SUB,HRU,YR,MO,DA")
      end if

      end
