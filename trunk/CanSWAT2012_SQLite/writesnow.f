      subroutine writesnow
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine write the specified snow water equivalent into external 
!!    text file for current simulation day

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    sr_print    |none          |print code: 0, print snow water equivalent of
!!                               |entire watershed
!!                               |i: print SWE of HRU i
!!    sr_flag     |none          |indicate whether snow redistribution occurs in 
!!                               |current day
!!    sno_hru(:)  |none          |snow water equivalent of each HRU in current day
!!    hru_km(:)   |km^2          |area of each HRU

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~
      use parm
      real, dimension (:), allocatable :: v
      integer :: ii
      
      if (sr_print > 0) then 
        allocate (v(sr_print))
      else
        allocate (v(1))
      end if
      
      select case (sr_code)
      case (0)     !! no snow redistribution at all
        if (sr_print ==0 ) then 
          v = sum (sno_hru * hru_km) * 1000         
        end if
        if (sr_print > 0) then 
          v = sno_hru(hru_output)
        end if  
        
      case (1)     !! proceed snow redistribution
        if (sr_print == 0) then
          if (sr_flag == 1) then 
            v = sum (sno_hru * hru_km) * 1000
          else
            v = 9999.99      !! default value
          end if
        end if
        if (sr_print > 0) then
!          if (sr_flag ==1 ) then
!            v = sno_hru(hru_output)
!          else
!            v = 9999.99      !! default value
!          end if
          v = sno_hru(hru_output)
        end if
      end select
      
      write (1, 1000, advance = 'no') iyr, immo-12*(curyr-1), i
      if (sr_print > 0) then      !! print results for specified HRU
        do ii = 1, sr_print       !! precipitation
          write (1, 2000, advance = 'no') subp(hru_output(ii))
        end do
        do ii = 1, sr_print       !! temperature
          write (1, 2000, advance = 'no') tmpav(hru_output(ii))
        end do
        do ii = 1, sr_print       !! wind speed
          write (1, 2000, advance = 'no') u10(hru_output(ii))
        end do
        do ii = 1, sr_print       !! WL
          if (sr_flag==0) then 
            write (1, 2000, advance = 'no') 9999.99
          else
            write (1, 2000, advance = 'no') sr_wl(hru_output(ii))
          end if          
        end do
        do ii = 1, sr_print       !! WT
          if (sr_flag==0) then 
            write (1, 2000, advance = 'no') 9999.99
          else
            write (1, 2000, advance = 'no') sr_wt(hru_output(ii))
          end if   
        end do
        do ii = 1, sr_print       !! WW
          if (sr_flag==0) then 
            write (1, 2000, advance = 'no') 9999.99
          else
            write (1, 2000, advance = 'no') sr_ww(hru_output(ii))
          end if
        end do
        if (sr_print == 1) then   !! snow water equivalent
          write (1, 2000) v
        else
          do ii = 1, sr_print - 1
            write (1, 2000, advance = 'no') v(ii)
          end do
          write (1, 2000) v(ii)
        end if
        
      else                        !! just print out the total volume
        write (1, '(f20.2)') v(1)
      end if
      
1000  format (3i5)     
2000  format (f10.2)     

      deallocate (v)
      
      return 
      end 