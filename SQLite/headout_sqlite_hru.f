      subroutine headout_sqlite_hru

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table hru in SQLite database
!!     The file handle for output.hru is 28

      use parm

      integer :: j
      integer :: colhrunum
      character(len=14) :: indexname

      !!hru table
      !!The number of common columns
      tblhru_num = 2 + datecol_num

      !!get number of columns of hru
      colhrunum = 0
      if (ipdvas(1) > 0) then
        colhrunum = tblhru_num + itots
      else
        colhrunum = tblhru_num + mhruo
      end if

      !!create table hru
      allocate( colhru(colhrunum) )
      call sqlite3_column_props( colhru(1), "LULC", SQLITE_CHAR,4)
      call sqlite3_column_props( colhru(2), "MGT", SQLITE_INT)
      call headout_sqlite_adddate(colhru,colhrunum,3)

      if (ipdvas(1) > 0) then
        do j = 1, itots
         call sqlite3_column_props(colhru(tblhru_num+j),heds(ipdvas(j)),
     &                                                      SQLITE_REAL)
        end do
      else
        do j = 1, mhruo
            call sqlite3_column_props(colhru(tblhru_num+j), heds(j),
     &                                                      SQLITE_REAL)
        end do
      end if

      !table name
      allocate(tblhru(nhru))
      do j = 1, subtot
        write(tblhru(j),5000) j
        write(*,*) tblhru(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblhru(j))
        call sqlite3_create_table( db, tblhru(j), colhru )

        write(indexname,5001) j
        write(*,*) indexname
        call headout_sqlite_createindex(indexname,tblhru(j),"",1)
      end do
      return

5000  format ('hru',i5.5)
5001  format ('hru',i5.5,'_index')

      end
