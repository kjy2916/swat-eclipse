      subroutine headout_sqlite_mgt

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create three tables in SQLite database for management data
!!     which is previously in output.mgt
!!     The file handle for output.mgt is 143

      use parm

      integer :: j,basiccolnum,valuecolnum

      tblmgt = 'mgt'

      call sqlite3_delete_table( db, tblmgt)

      if(imgt == 1) then
          valuecolnum = size(hedmgt)
          basiccolnum = 7
          allocate( colmgt(basiccolnum + valuecolnum + 2) )
          call sqlite3_column_props(colmgt(1),"SUB",SQLITE_INT)
          call sqlite3_column_props(colmgt(2),"HRU",SQLITE_INT)
          call sqlite3_column_props(colmgt(3),"YR",SQLITE_INT)
          call sqlite3_column_props(colmgt(4),"MO",SQLITE_INT)
          call sqlite3_column_props(colmgt(5),"DA",SQLITE_INT)
          call sqlite3_column_props(colmgt(6),"CROP_FERT_PEST",
     &                                               SQLITE_CHAR,10)
          call sqlite3_column_props(colmgt(7),"OPERATION",
     &                                               SQLITE_CHAR,10)
          do j=1,valuecolnum
            call sqlite3_column_props( colmgt(basiccolnum + j),
     &                                          hedmgt(j), SQLITE_REAL)
          end do
          call sqlite3_column_props(colmgt(basiccolnum +valuecolnum +1),
     &                                               "IRRSC",SQLITE_INT)
          call sqlite3_column_props(colmgt(basiccolnum +valuecolnum +2),
     &                                               "IRRNO",SQLITE_INT)

          call sqlite3_create_table( db, tblmgt, colmgt )
          call sqlite3_create_index( db, "mgt_index",tblmgt,
     &                                              "SUB,HRU,YR,MO,DA")

      end if
      end
