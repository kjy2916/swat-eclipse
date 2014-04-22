      subroutine snoww
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine calculate the three types of weights WT, WL, and 
!!    WW, as well as W for each HRU in curent day.

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    nhru        |none          |total number of HRU in watershed
!!    wt_k1       |none          |wt coefficient
!!    wt_k2       |none          |wt coefficient
!!    hru_wndslp(:)|none         |slope along wind speed
!!    hru_curv(:) |none          |culvate of each HRU
!!    hru_shc_con(:)  |mm        |snow water holding capacity for conventional till
!!    hru_shc_no(:)  |mm         |snow water holding capacity for no till
!!    sno_hru2(:) |mm            |swe in each HRU at previous day
!!    wl_shccrop  |mm            |crop field snow water holding capacity
!!    ww_uto      |              |ww coefficient
!!    ww_aa       |              |ww coefficient
!!    k_blow      |none          |fraction of snow is blow in/out of watershed
!!    rsno_fac    |(mm/mm/°C/day)|rain on snow impact factor
!!    ww_to       |oC            |ww coefficient
!!    tmpav(:)    |oC            |average temperature in current day
!!    u10(:)      |m/s           |wind speed of each HRU in current day
!!    hru_km(:)   |km^2          |area of each HRU

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    sr_wt(:)    |none          |topology weight of each HRU in current day
!!    sr_wl(:)    |none          |land cover weight of each HRU in current day
!!    sr_ww(:)    |none          |wind, temperature weight of each HRU in
!!                               |current day
!!    sr_w(:)     |km^2          |weight to calculate snow redistribution amount

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    ii          |none          |counter
!!    ww_ut       |m/s           |ww calculation
!!    ww_tmp      |m/s           |ww calculation

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~
      use parm
      integer :: ii
      real :: ww_ut, ww_tmp
      real :: hru_shc
      integer :: landid, tillcode
      
!!    calculate WT, may change daily if wind direction is changed
      sr_wt = 1 + wt_k1 * hru_wndslp + wt_k2 * hru_curv
      
!!    calculate WL, change daily
      
      do ii = 1, nhru
      
        !! reset sr_wt if negative, May 12 2011
        if (sr_wt(ii) < 0) sr_wt(ii) = 0.1
      
        !! update hru_shc(:)
        tillcode = idtill(nro(ii), ntil(ii), ii)        
        landid = idplt(nro(ii), icr(ii), ii)
        if (tillcode == 0) tillcode = ztcode
        if (tillcode == ztcode) then
          hru_shc = hru_shc_no(ii)
       !!yongbo
            !call modify_usle_k(usle_k(ii) * usle_kc)
            !erorgn(ii) = erorgn_r     !! change erorgn(j)        
            !erorgp(ii) = erorgp_r      !! change erorgp(j)       
            !phoskd = phoskd_r         !! change phoskd       
            !nperco = nperco_r         !! change nperco
            !usle_p(ii) = usle_pr
            !pperco = pperco_r
            !psp = psp_r
            !rsdco = rsdco_r
      !!yongbo  
        else
          hru_shc = hru_shc_con(ii)
      !!yongbo  
          !call modify_usle_k(usle_kb(ii))
          !erorgn(ii) = erorgn_b(ii)   !! change erorgn(j) back       
          !erorgp(ii) = erorgp_b(ii)   !! change erorgp(j) back      
          !phoskd = phoskd_b         !! change phoskd back         
          !nperco = nperco_b         !! change nperco back 
          !usle_p(ii) = usle_pb(ii)
          !pperco = pperco_b
          !psp = psp_b
          !rsdco = rsdco_b
      !!yongbo


        end if

        !! calculate WL for each HRU in current day
        if (sno_hru2(ii) > hru_shc) then
          sr_wl(ii) = 1
        else
          sr_wl(ii) = hru_shc / wl_shccrop + sno_hru2(ii) *             &
     &                      (1 - hru_shc / wl_shccrop) / hru_shc
        end if
      end do
      
!!    calculate WW, change daily
      do ii = 1, nhru
        ww_ut = ww_ut0 + ww_aa * (tmpav(ii) + ww_t0) **2
        ww_tmp = u10(ii) - ww_ut
        if (ww_tmp < 0) then
          sr_ww(ii) = 0
        elseif (ww_tmp < ww_u0 .and. ww_tmp > 0) then 
          sr_ww(ii) = ww_tmp / ww_u0
        else 
          sr_ww(ii) = 1
        end if
      end do
      
!!    calculate W, change daily
      sr_w = sr_wt * sr_wl * hru_km
      
      return       
      end

 
