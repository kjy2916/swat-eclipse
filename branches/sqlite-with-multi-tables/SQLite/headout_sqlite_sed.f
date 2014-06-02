      subroutine headout_sqlite_sed

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table sed in SQLite database for reach sediment
!!     The file handle for output.sed is 84

      use parm

      integer :: j,sedbasiccolnum,sedvaluecolnum
      character(len=12) :: indexname

      sedbasiccolnum = datecol_num
      sedvaluecolnum = size(hedsed)

      allocate( colsed(sedbasiccolnum + sedvaluecolnum) )

      call headout_sqlite_adddate(colsed,
     &          sedbasiccolnum + sedvaluecolnum,1)

      do j=1,sedvaluecolnum
        call sqlite3_column_props( colsed(sedbasiccolnum + j),
     &                                          hedsed(j), SQLITE_REAL)
      end do

      allocate(tblsed(subtot))
      do j = 1, subtot
        write(tblsed(j),5000) j
        write(*,*) tblsed(j)

        !!delete any existing table
        call sqlite3_delete_table( db, tblsed(j))
        call sqlite3_create_table( db, tblsed(j), colsed )

        write(indexname,5001) j
        write(*,*) indexname
        call headout_sqlite_createindex(indexname,tblsed(j),"",1)
      end do
      return

5000  format ('sed',i3.3)
5001  format ('sed',i3.3,'_index')
      end
