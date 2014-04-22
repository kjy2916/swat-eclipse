      subroutine readsnowrd
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine read snow redistribution required data stored in file 
!!    hru_sr.txt and snow.txt

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    nhru        |none          |total number of HRU in watershed
!!
!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    hru_wndslp(:)|none         |slope along wind direction of each HRU
!!    hru_curv(:) |none          |curvate of each HRU
!!    shc_ct(:)   |mm            |snow holding capacity of land id, conv. tillage
!!    shc_zt(:)   |mm            |snow holding capacity of land id, zero tillage
!!    wt          |none          |weight due to topology, i.e. slope and curvate
!!    wt_k1       |none          |coefficient in calculating wt
!!    wt_k2       |none          |coefficient in calculating wt
!!    ww_ut       |none          |coefficient in calculating ww
!!    ww_ut0      |none          |coefficient in calculating ww
!!    ww_t0       |none          |coefficient in calculating ww
!!    ww_aa       |none          |coefficient in calculating ww
!!    ww_init_swe |none          |coefficient in calculating ww
!!    wl_shccrop  |none          |coefficient in calculating wl
!!    k_blow      |none          |fraction of snow is blow in/out of watershed
!!    rsno_fac    |mm/mm/oc/day  |rain on snow impact factor

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    ii          |none          |counter
!!    iisub       |none          |subbasin index, tmp variable
!!    iiihru      |none          |hru index, tmp variable 



!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~      
      use parm
      
      integer :: ii, iisub, iiihru
      real :: frac
      character (len=80) :: titldum
      integer :: stat
      integer :: shc1
      real :: shc2, shc3
      
!!    read data from hru_sr.txt      
      open (100, file = "hru_sr.txt")
      read (100, 5101) titldum
      do ii = 1, nhru
        read (100, *) iisub, iiihru, hru_wndslp(ii), hru_curv(ii),       &
     &                                    hru_shc_con(ii),hru_shc_no(ii)
        
      end do
      close (100)

!!    read data from snow.txt
      open (100, file = "snow.txt")
      read (100, *) sr_code       !! read sr_code
      if (sr_code == 0) then
        write (*,*) "Snow Redistribution is off."
      else
        write (*,*) "Snow Redistribution is on."
      end if

      read (100, *) sr_print      !! read sr_print
      if (sr_print > 0) then 
        allocate (hru_output(sr_print))
        read (100, *) (hru_output(ii), ii=1, sr_print)
      elseif (sr_print==0) then
        read (100, 5101) titldum  !! skip this line
      end if         
      
      read (100, *) ztcode        !! zero tillage code, 108
      read (100, *) wt_k1         !! read k1
      read (100, *) wt_k2         !! read k2
      read (100, *) ww_ut0        !! read Ut0
      read (100, *) ww_u0         !! read U0
      read (100, *) ww_t0         !! read to
      read (100, *) ww_aa         !! read aa
      read (100, *) ww_init_swe   !! read initial snow water equivalent
      read (100, *) k_blow        !! read fraction
      read (100, *) wl_shccrop    !! crop snow holding capacity  
      read (100, *) rsno_fac      !! read rain on snow impact factor      
      close (100)
      
!!    open snowcomp.txt for write snow water equivalent of the entire watershed
      open (1, file = "snowcomp.txt")
      write (1, '(3a6)', advance='no') 'year ', 'month ', 'day '
      
      if (sr_print > 0) then
        do ii = 1, sr_print         !! precipitation
          write (1, 5000, advance='no') 'prec_', hru_output(ii)
        end do
      
        do ii = 1, sr_print         !! temperature 
          write (1, 5000, advance='no') 'temp_', hru_output(ii)
        end do
      
        do ii = 1, sr_print         !! wind speed
          write (1, 5000, advance='no') 'wind_', hru_output(ii)
        end do
      
        do ii = 1, sr_print         !! land cover weight
          write (1, 5000, advance='no') 'WL_', hru_output(ii)
        end do
      
        do ii = 1, sr_print         !! topology weight
          write (1, 5000, advance='no') 'WT_', hru_output(ii)
        end do
      
        do ii = 1, sr_print         !! wind and temperature weight
          write (1, 5000, advance='no') 'WW_', hru_output(ii)
        end do
      
        if (sr_print == 1) then
          write (1, 5000) 'SWE_', hru_output(1)
        else
          do ii = 1, sr_print-1     !! snow water equivalent
            write (1, 5000, advance='no') 'SWE_', hru_output(ii)
          end do
          write (1, 5000) 'SWE_', hru_output(ii)
        end if
      
      else 
        write (1, '(a10)') 'TOTAL_VOL'
      end if
            
!!    init variables for snow redistribution
      snowday = 0
      sno_hru2 = 0.
      
5000  format (a, i0, 2x)                  
5101  format (a80)      
      return
      end
