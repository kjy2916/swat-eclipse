      subroutine headout_sqlite_mgt

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create three tables in SQLite database for management data
!!     which is previously in output.mgt
!!     The file handle for output.mgt is 143

      use parm

      integer :: j,basiccolnum,valuecolnum
      character(len=14) :: indexname

      !!allocate table names for all hrus
      allocate(tblmgt(nhru))
      do j = 1, nhru
        write(tblmgt(j),5000) j
        write(*,*) tblmgt(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblmgt(j))
      end do

      if(imgt == 1) then
          valuecolnum = size(hedmgt)
          basiccolnum = 5
          allocate( colmgt(basiccolnum + valuecolnum + 2) )
          call sqlite3_column_props(colmgt(1),"YR",SQLITE_INT)
          call sqlite3_column_props(colmgt(2),"MO",SQLITE_INT)
          call sqlite3_column_props(colmgt(3),"DA",SQLITE_INT)
          call sqlite3_column_props(colmgt(4),"CROP_FERT_PEST",
     &                                               SQLITE_CHAR,10)
          call sqlite3_column_props(colmgt(5),"OPERATION",
     &                                               SQLITE_CHAR,10)
          do j=1,valuecolnum
            call sqlite3_column_props( colmgt(basiccolnum + j),
     &                                          hedmgt(j), SQLITE_REAL)
          end do
          call sqlite3_column_props(colmgt(basiccolnum +valuecolnum +1),
     &                                               "IRRSC",SQLITE_INT)
          call sqlite3_column_props(colmgt(basiccolnum +valuecolnum +2),
     &                                               "IRRNO",SQLITE_INT)


          do j = 1, nhru
            call sqlite3_create_table( db, tblmgt(j), colmgt )

            write(indexname,5001) j
            write(*,*) indexname
            call headout_sqlite_createindex(indexname,tblmgt(j),
     &                                              "YR,MO,DA",0)
          end do
      end if
      return

5000  format ('mgt',i5.5)
5001  format ('mgt',i5.5,'_index')
      end
