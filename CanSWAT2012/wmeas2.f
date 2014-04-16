      subroutine wmeas2

!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine reads in wind speed data from file and assigns the
!!    data to HRUs only for test purpose, where wind speed is the same for 
!!    the entire watershed for current simulation day

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    nhru        |none          |number of HRU in the watershed

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    u10(:)      |m/s           |wind speed for the day in HRU  

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    iiyr        |none          |year in external wind file
!!    iimon       |none          |month in external wind file
!!    iiday       |none          |day in external wind file
!!    vms         |m/s           |wind speed in external wind file
!!    ii          |none          |counter
           
!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~
      use parm
      integer :: iiyr, iimon, iiday, ii
      real :: vws
      
!! the header is already read while the file is open
      
!!    read a single row from wind external data file      
      read (139, *) iiyr, iimon, iiday, vws
      
!!    set this single value to every HRU
      do ii = 1, nhru
        u10(ii) = vws
      end do
      
      return       
      end