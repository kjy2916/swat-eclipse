      subroutine headout_sqlite_rch

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

      integer :: j
      integer :: colreachnum,colnum

      !!The number of common columns
      colnum = 0
      if (icalen == 0) then
        colnum = 2
      else if (icalen == 1) then
        colnum = 4
      end if

      !!get number of columns of reach
      colreachnum = 0
      if (ipdvar(1) > 0) then
        colreachnum = colnum + itotr
      else
        colreachnum = colnum + mrcho
      end if

      !!create table rch
      allocate( colreach(colreachnum) )
      call sqlite3_column_props( colreach(1), "RCH", SQLITE_INT)
      if(icalen == 0) then
        call sqlite3_column_props( colreach(2), "MON", SQLITE_INT)
      else if(icalen == 1) then
        call sqlite3_column_props( colreach(2), "YR", SQLITE_INT)
        call sqlite3_column_props( colreach(3), "MO", SQLITE_INT)
        call sqlite3_column_props( colreach(4), "DA", SQLITE_INT)
      end if
      if (ipdvar(1) > 0) then
        do j = 1, itotr
           call sqlite3_column_props(colreach(colnum+j),hedr(ipdvar(j)),
     &                                                      SQLITE_REAL)
        end do
      else
        do j = 1, mrcho
            call sqlite3_column_props(colreach(colnum+j), hedr(j),
     &                                                      SQLITE_REAL)
         end do
      end if
      call sqlite3_delete_table( db, "rch")
      call sqlite3_create_table( db, "rch", colreach )
      end
