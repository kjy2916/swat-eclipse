      subroutine dailycn2

!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    Calculates curve number for the day in the HRU under frozen soil condition

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    ihru        |none          |HRU number
!!    sci(:)      |none          |retention coefficient for cn method based on 
!!                               |plant ET
!!    smx(:)      |none          |retention coefficient for cn method based on
!!                               |soil moisture
!!    sol_sw(:)   |mm H2O        |amount of water stored in soil profile on
!!                               |any given day
!!    sol_tmp(2,:)|deg C         |daily average temperature of second soil layer
!!    wrt(1,:)    |none          |1st shape parameter for calculation of
!!                               |water retention
!!    wrt(2,:)    |none          |2nd shape parameter for calculation of
!!                               |water retention
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    cnday(:)    |none          |curve number for current day, HRU and at 
!!                               |current soil moisture
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    icn         |none          |CN method flag:
!!                               |(for testing alternative method)
!!                               |0 use traditional SWAT method which bases
!!                               |  CN on soil moisture
!!                               |1 use alternative method which bases CN on
!!                               |  plant ET
!!    j           |none          |HRU number
!!    r2          |none          |retention parameter in CN equation
!!    xx          |none          |variable used to store intermediate
!!                               |calculation result
!!    sol_fcv     |none          |soil filed capacity value for fs calculation
!!    sol_ulv     |              |saturated soil water content for fs calculation
!!    sol_stv     |              |soil water content for fs calculation

!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ SUBROUTINES/FUNCTIONS CALLED ~ ~ ~
!!    Intrinsic: Exp


!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~


      use parm

      integer :: j   

      real :: xx, r2
      
      !! added for new wrt(1,j) and wrt(2,j)
      real :: s3, rto3, rtos     
      real :: sol_fcv, sol_ulv, sol_stv
      
      real :: wrt1, wrt2

      j = 0
      j = ihru


      xx = 0.
      r2 = 0.
      
      !! fs_flag will be greater than zero, otherwise, this subroutine will 
      !! not be called
      sol_fcv = sum(sol_fc(1:fs_flag(j), j))
      sol_ulv = sum(sol_ul(1:fs_flag(j), j))
      sol_stv = sum(sol_st(1:fs_flag(j), j))
!      sol_fcv = sum(sol_fc(1:fs_flag(2), j))
!      sol_ulv = sum(sol_ul(1:fs_flag(2), j))
!      sol_stv = sum(sol_st(1:fs_flag(2), j))
!!    extracted from source file curno.f for new wrt(1,j) and wrt(2,j)
!!    the right previous call to curno.f is in schedule_ops.f, called 
!!    in turn in subbasin.f line 202

      !! calculate retention parameter value for CN3
      s3 = 254. * (100. / cn3(j) - 1.)
      
      !! calculate fraction difference in retention parameters
      rto3 = 0.
      rtos = 0.
      rto3 = 1. - s3 / smx(j)
      rtos = 1. - 2.54 / smx(j)
      
      !! calculate shape parameters
      !! call ascrv(rto3,rtos,sol_sumfc(j),sol_sumul(j),wrt(1,j),wrt(2,j))
!      call ascrv(rto3,rtos,sol_fcv,sol_ulv,wrt(1,j),wrt(2,j))
      call ascrv(rto3,rtos,sol_fcv,sol_ulv,wrt1,wrt2)
      !! xx = wrt(1,j) - wrt(2,j) * sol_sw(j)
!      xx = wrt(1,j) - wrt(2,j) * sol_stv
      xx = wrt1 - wrt2 * sol_stv
      if (xx < -20.) xx = -20.
      if (xx > 20.) xx = 20.

      if (icn <= 0) then
        !! traditional CN method (function of soil water)
        if ((sol_stv + Exp(xx)) > 0.001) then
          r2 = smx(j) * (1. - sol_stv / ( sol_stv + Exp(xx)))
        end if
      else                                                     
        !! alternative CN method (function of plant ET)
        r2 = amax1(3., sci(j))
      end if

    !  if (sol_tmp(2,j) <= 0.) r2 = smx(j) * (1. - Exp(- cn_froz * r2))
      r2 = smx(j) * (1. - Exp(- cn_froz * r2))
      r2 = amax1(3.,r2)

      cnday2(j) = 25400. / (r2 + 254.)
      cnday(j) = max(cnday(j), cnday2(j))
      sol_cnsw(j) = sol_sw(j)


      return
      end
