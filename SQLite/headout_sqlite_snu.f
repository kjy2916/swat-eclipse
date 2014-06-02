      subroutine headout_sqlite_snu

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create three tables in SQLite database for management data
!!     which is previously in output.snu (output.sol)
!!     The file handle for output.snu is 121

      use parm

      integer :: j,basiccolnum,valuecolnum
      character(len=14) :: indexname

      !!allocate table names for all hrus
      allocate(tblsnu(nhru))
      do j = 1, nhru
        write(tblsnu(j),5000) j
        write(*,*) tblsnu(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblsnu(j))
      end do

      if(isol == 1) then
          valuecolnum = size(hedsnu)
          basiccolnum = 3
          allocate( colsnu(basiccolnum + valuecolnum) )
          call sqlite3_column_props(colsnu(1),"YR",SQLITE_INT)
          call sqlite3_column_props(colsnu(2),"MO",SQLITE_INT)
          call sqlite3_column_props(colsnu(3),"DA",SQLITE_INT)
          do j=1,valuecolnum
            call sqlite3_column_props( colsnu(basiccolnum + j),
     &                                          hedsnu(j), SQLITE_REAL)
          end do

          do j = 1, nhru
            call sqlite3_create_table( db, tblsnu(j), colsnu )

            write(indexname,5001) j
            write(*,*) indexname
            call headout_sqlite_createindex(indexname,tblsnu(j),
     &                                              "YR,MO,DA",0)
          end do
      end if
      return

5000  format ('snu',i5.5)
5001  format ('snu',i5.5,'_index')
      end
