      subroutine headout_sqlite_wtr

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     this subroutine writes the headings to the major output files

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    hedb(:)     |NA            |column titles in subbasin output files
!!    hedr(:)     |NA            |column titles in reach output files
!!    hedrsv(:)   |NA            |column titles in reservoir output files
!!    heds(:)     |NA            |column titles in HRU output files
!!    hedwtr(:)   |NA            |column titles in HRU impoundment output
!!                               |file
!!    icolb(:)    |none          |space number for beginning of column in 
!!                               |subbasin output file
!!    icolr(:)    |none          |space number for beginning of column in
!!                               |reach output file
!!    icolrsv(:)  |none          |space number for beginning of column in
!!                               |reservoir output file
!!    icols(:)    |none          |space number for beginning of column in
!!                               |HRU output file
!!    ipdvab(:)   |none          |output variable codes for output.sub file
!!    ipdvar(:)   |none          |output variable codes for .rch file
!!    ipdvas(:)   |none          |output variable codes for output.hru file
!!    isproj      |none          |special project code:
!!                               |1 test rewind (run simulation twice)
!!    itotb       |none          |number of output variables printed (output.sub)
!!    itotr       |none          |number of output variables printed (.rch)
!!    itots       |none          |number of output variables printed (output.hru)
!!    msubo       |none          |maximum number of variables written to
!!                               |subbasin output file (output.sub)
!!    mhruo       |none          |maximum number of variables written to 
!!                               |HRU output file (output.hru)
!!    mrcho       |none          |maximum number of variables written to
!!                               |reach output file (.rch)
!!    prog        |NA            |program name and version
!!    title       |NA            |title lines from file.cio
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    ilen        |none          |width of data columns in output file
!!    j           |none          |counter
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~


!!    ~ ~ ~ SUBROUTINES/FUNCTIONS CALLED ~ ~ ~
!!    header

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~

      use parm

      integer :: j,wtrbasiccolnum,wtrvaluecolnum

      tblwtr = 'wtr'
      wtrbasiccolnum = 0
      wtrvaluecolnum = 40

      !!create table rsv
      if (iwtr == 1) then
          if(iprint < 2) then
            wtrbasiccolnum = 6
          else
            wtrbasiccolnum = 5
          end if
          allocate( colwtr(wtrbasiccolnum + wtrvaluecolnum) )

          call sqlite3_column_props( colwtr(1), "LULC", SQLITE_CHAR,4)
          call sqlite3_column_props( colwtr(2), "HRU", SQLITE_INT)
          call sqlite3_column_props( colwtr(3), "SUB", SQLITE_INT)
          call sqlite3_column_props( colwtr(4), "MGT", SQLITE_INT)
          call sqlite3_column_props( colwtr(5), "YR", SQLITE_INT)
          if(iprint < 2) then
            call sqlite3_column_props( colwtr(6), "MON", SQLITE_INT)
          end if
          do j = 1, wtrvaluecolnum
             call sqlite3_column_props(colwtr(wtrbasiccolnum+j),
     &                                          hedwtr(j),SQLITE_REAL)
          end do
          call sqlite3_delete_table( db, tblwtr)
          call sqlite3_create_table( db, tblwtr, colwtr )
      end if


      end
