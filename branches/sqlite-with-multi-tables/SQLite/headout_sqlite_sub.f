      subroutine headout_sqlite_sub

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table sub in SQLite database for subbasin
!!     The file handle for output.sub is 31

      use parm

      integer :: j,colsubnum
      character(len=12) :: indexname

      !!subbasin table
      !!The number of common columns
      tblsub_num = datecol_num

      !!get number of columns of sub
      colsubnum = 0
      if (ipdvab(1) > 0) then
        colsubnum = tblsub_num + itotb
      else
        colsubnum = tblsub_num + msubo
      end if

      !!create table sub
      allocate( colsub(colsubnum) )
      call headout_sqlite_adddate(colsub,colsubnum,1)

      if (ipdvab(1) > 0) then
        do j = 1, itotb
         call sqlite3_column_props(colsub(tblsub_num+j),hedb(ipdvab(j)),
     &                                                      SQLITE_REAL)
        end do
      else
        do j = 1, msubo
            call sqlite3_column_props(colsub(tblsub_num+j), hedb(j),
     &                                                      SQLITE_REAL)
        end do
      end if

      !table name
      allocate(tblsub(subtot))
      do j = 1, subtot
        write(tblsub(j),5000) j
        write(*,*) tblsub(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblsub(j))
        call sqlite3_create_table( db, tblsub(j), colsub )

        write(indexname,5001) j
        write(*,*) indexname
        call headout_sqlite_createindex(indexname,tblsub(j),"",1)
      end do
      return

5000  format ('sub',i3.3)
5001  format ('sub',i3.3,'_index')
      end
