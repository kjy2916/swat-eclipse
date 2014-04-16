      subroutine snowf
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine finish the snow redistribution process. May 13 2011

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    nhru        |none          |total number of HRU in watershed
!!    hru_km(:)   |km^2          |area of each HRU
!!    sno_hru(:)  |mm            |swe of each HRU in current day

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    sno_hru2(:) |mm            |swe of each HRU in previous day

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    ii          |none          |loop counter
!!    neg_vol     |10^3 m^3      |sum of negative volume
!!    pos_area    |km^2          |sum of positive sno_hru(:) area

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~  
      use parm
      integer :: ii
      real :: neg_vol, pos_area
            
!!    conduct water rebalance to eliminate negative swe among HRUs
!!    calculate negative sno_hru water volume, and positive area   
!!    Based on Yongbo's comment, the rebalance due to negative sno_hru
!!    is dropped. May 12 2011  

!      neg_vol = 0.
!      pos_area = 0.
!      
!      do ii = 1, nhru
!        if (sno_hru(ii) < 0) then 
!          neg_vol = neg_vol + sno_hru(ii) * hru_km(ii)     !! 10^3 m^3
!        else
!          pos_area = pos_area + hru_km(ii)
!        end if
!      end do
!
!!!    alloate this negative volume evenly to positive sno_hru HRU      
!      do ii = 1, nhru
!        if (sno_hru(ii) < 0) then 
!          sno_hru(ii) = 0.
!        else
!          sno_hru(ii) = sno_hru(ii) + neg_vol / pos_area   !! mm
!        end if
!      end do      

!!    reset previous day swe
      sno_hru2 = sno_hru

      !!Zhiqiang, 2014-2-28
      !!calculate subbasin swe
      sr_sub_swe = 0.
      if(sr_flag == 1) then
          do ii = 1, nhru
            sr_sub_swe(hru_sub(ii)) = sr_sub_swe(hru_sub(ii)) +
     &                 (sno_hru2(ii) * hru_km(ii)) / sub_km(hru_sub(ii))
          end do
      end if

      !!Zhiqiang, 2014-2-28

!!    write swe to external text file
      call writesnow
      
      return
      end
