      subroutine headout_sqlite_sed

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

      !!File handle = 84
      use parm

      integer :: j,sedbasiccolnum,sedvaluecolnum
      character(len=13) :: hedsed(19)

      tblsed = 'sed'
      hedsed=(/ "  SED_INtons"," SED_OUTtons",
     &          " SAND_INtons","SAND_OUTtons",
     &          " SILT_INtons","SILT_OUTtons",
     &          " CLAY_INtons","CLAY_OUTtons",
     &          " SMAG_INtons","SMAG_OUTtons",
     &          "  LAG_INtons"," LAG_OUTtons",
     &          "  GRA_INtons"," GRA_OUTtons",
     &          "  CH_BNKtons","  CH_BEDtons",
     &          "  CH_DEPtons","  FP_DEPtons",
     &          "     TSSmg_L"/)

      sedbasiccolnum = 2
      sedvaluecolnum = size(hedsed)

      !!create table rsv
      if(iprint == 0) sedbasiccolnum = 3
      if(iprint == 1) sedbasiccolnum = 4
      allocate( colsed(sedbasiccolnum + sedvaluecolnum) )

      call sqlite3_column_props( colsed(1), "RCH", SQLITE_INT)
      call sqlite3_column_props( colsed(2), "YR", SQLITE_INT)
      if(iprint < 2) then       !!monthly or daily
        call sqlite3_column_props( colsed(3), "MO", SQLITE_INT)
        if(iprint == 1) then    !!daily
            call sqlite3_column_props( colsed(4), "DA", SQLITE_INT)
        end if
      end if

      do j=1,sedvaluecolnum
        call sqlite3_column_props( colsed(sedbasiccolnum + j),
     &                                          hedsed(j), SQLITE_REAL)
      end do

      call sqlite3_delete_table( db, tblsed)
      call sqlite3_create_table( db, tblsed, colsed )
      end
