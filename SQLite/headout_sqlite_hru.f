      subroutine headout_sqlite_hru

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
      integer :: colhrunum

      tblhru = 'hru'

      !!delete any existing table
      call sqlite3_delete_table( db, tblhru)

      !!hru table
      !!The number of common columns
      tblhru_num = 0
      if (icalen == 0) then
        if(iprint == 2) then    !!yearly output
            tblhru_num = 5
        else                    !!daily or monthly output
            tblhru_num = 6
        end if
      else if (icalen == 1) then
        tblhru_num = 7
      end if

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
      call sqlite3_column_props( colhru(2), "HRU", SQLITE_INT)
      call sqlite3_column_props( colhru(3), "SUB", SQLITE_INT)
      call sqlite3_column_props( colhru(4), "MGT", SQLITE_INT)
      if(icalen == 0) then
        call sqlite3_column_props( colhru(5), "YR", SQLITE_INT)
        if(iprint < 2) then
            call sqlite3_column_props( colhru(6), "MON", SQLITE_INT)
        end if
      else if(icalen == 1) then
        call sqlite3_column_props( colhru(5), "YR", SQLITE_INT)
        call sqlite3_column_props( colhru(6), "MO", SQLITE_INT)
        call sqlite3_column_props( colhru(7), "DA", SQLITE_INT)
      end if
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
      call sqlite3_create_table( db, tblhru, colhru )

      !create index
      if(icalen == 0) then
        call sqlite3_create_index( db, "hru_index", tblhru,
     &                                                 "HRU,SUB,YR,MON")
      else if(icalen == 1) then
         call sqlite3_create_index( db, "hru_index", tblhru,
     &                                               "HRU,SUB,YR,MO,DA")
      end if

      end
