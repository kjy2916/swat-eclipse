      subroutine headout_sqlite_deg

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create three tables in SQLite database for channel dimension
!!     which is previously in chan.deg
!!     The file handle for chan.deg is 16

      use parm

      integer :: j,basiccolnum,valuecolnum

      tbldeg = 'channel_dimension'

      call sqlite3_delete_table( db, tbldeg)

      valuecolnum = size(heddeg)
      basiccolnum = 2
      allocate( coldeg(basiccolnum + valuecolnum) )
      call sqlite3_column_props(coldeg(1),"RCH",SQLITE_INT)
      call sqlite3_column_props(coldeg(2),"YR",SQLITE_INT)
      do j=1,valuecolnum
        call sqlite3_column_props( coldeg(basiccolnum + j),
     &                                          heddeg(j), SQLITE_REAL)
      end do

      call sqlite3_create_table( db, tbldeg, coldeg )
      call sqlite3_create_index( db, "deg_index",tbldeg,
     &                                              "RCH,YR")

      end
