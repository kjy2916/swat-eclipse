      subroutine headout_sqlite_rch

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table rch in SQLite database
!!     The file handle for output.rch is 7
      use parm

      integer :: j
      integer :: colrchnum
      character(len=12) :: indexname

      !!The number of common columns
      tblrch_num = datecol_num

      !!get number of columns of reach
      colrchnum = 0
      if (ipdvar(1) > 0) then
        colrchnum = tblrch_num + itotr
      else
        colrchnum = tblrch_num + mrcho
      end if

      !!create table rch
      allocate( colrch(colrchnum) )
      call headout_sqlite_adddate(colrch,colrchnum,1)

      if (ipdvar(1) > 0) then
        do j = 1, itotr
           call sqlite3_column_props(colrch(tblrch_num+j),
     &                                  hedr(ipdvar(j)), SQLITE_REAL)
        end do
      else
        do j = 1, mrcho
            call sqlite3_column_props(colrch(tblrch_num+j), hedr(j),
     &                                                      SQLITE_REAL)
         end do
      end if

      !table name
      allocate(tblrch(subtot))
      do j = 1, subtot
        write(tblrch(j),5000) j
        write(*,*) tblrch(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblrch(j))
        call sqlite3_create_table( db, tblrch(j), colrch )

        write(indexname,5001) j
        write(*,*) indexname
        call headout_sqlite_createindex(indexname,tblrch(j),"",1)
      end do
      return

5000  format ('rch',i3.3)
5001  format ('rch',i3.3,'_index')

      end
