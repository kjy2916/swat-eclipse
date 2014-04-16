      subroutine snowrd
!!    Added by Hailiang April 12 2011      
      
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine calculate the snow redistribution after snow melting and 
!!    sublimation (i.e. evapotranspiration)

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    nhru        |none          |total number of HRU
!!    ihru        |none          |current HRU index
!!    sno_hru(:)  |              |snow water equivalent in current day of each HRU
!!    sr_wt(:)    |              |topology weight of each HRU
!!    sr_wl(:)    |              |land cover weight of each HRU
!!    sr_ww(:)    |              |wind and temperature weight of each HRU

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    sno_hru(:)  |              |snow water equivalent in current day of each HRU

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    sum_w       |none          |sum of sr_w(:)
!!    sr_wr       |none          |relevant weight
!!    sr_sap      |              |potential snow accumulation
!!    sr_scp      |              |potential change of swe over time interval
!!    sr_sr       |              |snow redistribution amount

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~      
      use parm
      real :: sr_wr, sr_sap, sr_scp, sr_sr
      real :: sub_area
      
      !!get sub area
      sub_area = sub_km(hru_sub(ihru))

      !! calculate WR, change the number of hru to sub_area/area(i)
      sr_wr = sr_w(ihru) * sub_area / hru_km(ihru)
      
      !! calculate SAP, use sno_hru2
      sr_sap = sr_wr * sr_sub_swe(hru_sub(ihru))
      
      !! calculate SCP, use sno_hru2
      sr_scp = sr_sap - sno_hru2(ihru)
      
      !! calculate snow redistion amount
      sr_sr = sr_scp * sr_ww(ihru)           
      
      !! update snow water equivalent
      sno_hru(ihru) = sno_hru(ihru) + sr_sr

      !!if (sno_hru(ihru) < 0.) sno_hru(ihru) = 0
      
      return
      end
