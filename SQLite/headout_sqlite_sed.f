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

      use parm

      integer :: j,sedbasiccolnum,sedvaluecolnum
      character(len=13) :: hedsed(19)

      tblsed = 'sed'
      sedbasiccolnum = 2
      sedvaluecolnum = 19
      hedsed=(/"SED_INtons","SED_OUTtons","     PETmm","      ETmm",
     &         "      SWmm","    PERCmm","    SURQmm","    GW_Qmm",
     &         "    WYLDmm","  SYLDt/ha"," ORGNkg/ha"," ORGPkg/ha",
     &         "NSURQkg/ha"," SOLPkg/ha"," SEDPkg/ha"," LAT Q(mm)",
     &         "LATNO3kg/h","GWNO3kg/ha","CHOLAmic/L","CBODU mg/L",
     &         " DOXQ mg/L"," TNO3kg/ha"/)

!1080  format (t8,'RCH',t17,'GIS',t23,'MON',t31,'AREAkm2',
!     &t40,'SED_INtons',t51,'SED_OUTtons',t63,'SAND_INtons',t74,
!     &'SAND_OUTtons',t87,'SILT_INtons',t98,'SILT_OUTtons',t111,
!     &'CLAY_INtons',t122,'CLAY_OUTtons',t135,'SMAG_INtons',t146,
!     &'SMAG_OUTtons',t160,'LAG_INtons',t171,'LAG_OUTtons',t184,
!     &'GRA_INtons',t195,'GRA_OUTtons',t208,'CH_BNKtons',t220,
!     &'CH_BEDtons',t232,'CH_DEPtons',t244,'FP_DEPtons',t259,'TSSmg/L')

      !!create table rsv
      if(iprint < 2) sedbasiccolnum = 3
      allocate( colwtr(sedbasiccolnum + sedvaluecolnum) )

      call sqlite3_column_props( colwtr(1), "RCH", SQLITE_INT)
      call sqlite3_column_props( colwtr(2), "YR", SQLITE_INT)
      if(iprint < 2) then
        call sqlite3_column_props( colwtr(3), "MON", SQLITE_INT)
      end if

      call sqlite3_column_props( colwtr(3), "SED_INtons", SQLITE_REAL)


      call sqlite3_delete_table( db, tblwtr)
      call sqlite3_create_table( db, tblwtr, colwtr )


      end
