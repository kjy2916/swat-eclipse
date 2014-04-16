      subroutine snowrdflag
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine set the snow redistribution flag, to open/close snow
!!    redistribution process

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    sr_code     |none          |indicate whether snow redistribution
!!    nhru        |none          |total number of HRU
!!    subp(:)     |              |precipitation on current day of each HRU
!!    tmpav(:)    |oC            |average temperature on current day of each 
!!                               |HRU
!!    sftmp       |none          |snow fall temperature
!!    sno_hru2(:) |              |snow water equivalent on previous day of each 
!!                               |HRU
!!    ww_init_swe |none          |snow water equivalent on the first snow day

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    sr_flag     |none          |indicate whether snow redistribution based 
!!                               |on precipitation and temperature, and current
!!                               |snow water equivalent
!!    snowday     |none          |current snow day

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    ii          |none          |counter

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~
      use parm
      integer :: ii
      
      !! check whether the project require snow redistribution
      if (sr_code == 0) then 
        sr_flag = 0
        return
      end if
      
      !! close the flag if any swe of HRU is zero, sno_hru2 is initialized 
      !! in readsnowrd.f
      if (minval (sno_hru2) < 1.e-6) sr_flag = 0
      
      !! open the flag
      do ii = 1, nhru
        if (subp(ii) > 1.e-6 .and. tmpav(ii) < sftmp) sr_flag = 1
      end do
      
      if (sr_flag == 1) then 
        snowday = snowday + 1
      end if  
      
      if (snowday == 1) sno_hru2 = ww_init_swe
                  
      return
      end