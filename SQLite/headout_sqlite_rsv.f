      subroutine headout_sqlite_rsv

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create table rsv in SQLite database for reservoirs
!!     The file handle for output.rsv is 8

      use parm

      integer :: j,rsvbasiccolnum,rsvvaluecolnum

      tblrsv = 'rsv'

      !!delete any existing table
      call sqlite3_delete_table( db, tblrsv)

      rsvvaluecolnum = 41
      rsvbasiccolnum = 1 + datecol_num
      allocate( colrsv(rsvbasiccolnum + rsvvaluecolnum) )

      call sqlite3_column_props( colrsv(1), "RES", SQLITE_INT)
      call headout_sqlite_adddate(colrsv,2)

      do j = 1, rsvvaluecolnum
         call sqlite3_column_props(colrsv(rsvbasiccolnum+j),hedrsv(j),
     &                                                      SQLITE_REAL)
      end do
      call sqlite3_create_table( db, tblrsv, colrsv )
      call headout_sqlite_createindex("rsv_index",tblrsv,"RES")
      end
