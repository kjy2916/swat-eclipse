      subroutine headout_sqlite_swr

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create three tables in SQLite database for management data
!!     which is previously in output.swr (output.sol)
!!     The file handle for output.swr is 129

      use parm

      integer :: j,basiccolnum,valuecolnum
      character(len=14) :: indexname

      !!allocate table names for all hrus
      allocate(tblswr(nhru))
      do j = 1, nhru
        write(tblswr(j),5000) j
        write(*,*) tblswr(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblswr(j))
      end do

      if(isto > 0) then
          valuecolnum = size(hedswr)
          basiccolnum = 3
          allocate( colswr(basiccolnum + valuecolnum) )
          call sqlite3_column_props(colswr(1),"YR",SQLITE_INT)
          call sqlite3_column_props(colswr(2),"MO",SQLITE_INT)
          call sqlite3_column_props(colswr(3),"DA",SQLITE_INT)
          do j=1,valuecolnum
            call sqlite3_column_props( colswr(basiccolnum + j),
     &                                          hedswr(j), SQLITE_REAL)
          end do

          do j = 1, nhru
            call sqlite3_create_table( db, tblswr(j), colswr )

            write(indexname,5001) j
            write(*,*) indexname
            call headout_sqlite_createindex(indexname,tblswr(j),
     &                                              "YR,MO,DA",0)
          end do
      end if
      return

5000  format ('swr',i5.5)
5001  format ('swr',i5.5,'_index')
      end
