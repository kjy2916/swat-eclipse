          subroutine soil_np
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine modified soil and n, p paramters, including 
!!    usle_k(:), erorgn(:), erorgp(:), phosko, nperco

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~
      use parm
      
      integer :: j,tillcode
	
      j = ihru
      
      !Liu
        !tillcode = idtill(nro(j), ntil(j), j)     
        tillcode = idtill   
        if (tillcode == 0) tillcode = ztcode
        if (tillcode == ztcode) then
            !call modify_usle_k(usle_k(j) * usle_kc)
            erorgn(j) = erorgn_r     !! change erorgn(j)        
            erorgp(j) = erorgp_r      !! change erorgp(j)       
            phoskd = phoskd_r         !! change phoskd       
            nperco = nperco_r         !! change nperco
            usle_p(j) = usle_pr
            pperco = pperco_r
            psp = psp_r
            rsdco = rsdco_r
            p_updis = p_updis_r
            sol_solp(1,j) = sol_solp_r
            n_updis = n_updis_r
      !!Liu  
        else 
          !call modify_usle_k(usle_kb(j))
          erorgn(j) = erorgn_b(j)   !! change erorgn(j) back       
          erorgp(j) = erorgp_b(j)   !! change erorgp(j) back      
          phoskd = phoskd_b         !! change phoskd back         
          nperco = nperco_b         !! change nperco back 
          usle_p(j) = usle_pb(j)
          pperco = pperco_b
          psp = psp_b
          rsdco = rsdco_b
          p_updis = p_updis_b
          sol_solp(1,j) = sol_solp_b(j)
          n_updis = n_updis_b
        end if
   
    !   Forage-notill
        !if (idplt(1,1,ihru)==12) then
        if (idplt(ihru)==12) then
        sol_solp(1,j)=sol_solp(1,j)/2.0
        end if

      !!yongbo

      if (sol_tmp(1, j) < 0) then
        call modify_usle_k(usle_k(j) * usle_kc)
        
        !erorgn(j) = erorgn_r      !! change erorgn(j)        
        !erorgp(j) = erorgp_r      !! change erorgp(j)       
        !phoskd = phoskd_r         !! change phoskd       
        !nperco = nperco_r         !! change nperco
 
      else
        
        call modify_usle_k(usle_kb(j))
     
        !erorgn(j) = erorgn_b(j)   !! change erorgn(j) back       
        !erorgp(j) = erorgp_b(j)   !! change erorgp(j) back      
        !phoskd = phoskd_b         !! change phoskd back         
        !nperco = nperco_b         !! change nperco back 
        
      end if   
      !!yongbo 
      
      return 
      end
      
      subroutine modify_usle_k(x)
!!    this subroutine modifies the value of usle_k for current HRU and current 
!!    day, and the parameters that usle_k impacted upto the call to this sub-
!!    routine
      use parm
      
      real, intent (in) :: x
      integer :: j
      j = ihru
      
      !! modify subsequent parameters related upto this point, in soil_phys.f
      usle_mult(j) = sol_rock(1,j) * x * usle_p(j) * usle_ls(j) * 11.8
        
      !! extracted from schedule_ops.f
      iops = ioper2(j)

      do while (iida == iopday(iops,j).and.iyr == iopyr(iops,j))

        select case(mgt_ops(iops,j))
        case (1)
        xm = 0.6 * (1. - Exp(-35.835 * hru_slp(j)))    
        sin_sl = Sin(Atan(hru_slp(j)))
        usle_ls(j) = (terr_sl(iops,j)/22.128) ** xm * (65.41 *          &
     &                    sin_sl * sin_sl + 4.56 * sin_sl + .065)
        usle_mult(j) = sol_rock(1,j) * x *                              &
     &                      terr_p(iops,j) * usle_ls(j) * 11.8

         
        case (3)
        usle_mult(j) = usle_mult(j) * cont_p(iops,j) / usle_p(j)
          
        case (5)
        usle_mult(j) = usle_mult(j) * strip_p(iops,j) / usle_p(j)
          
        end select

        ioper2(j) = ioper2(j) + 1
        iops = ioper2(j)

      end do      
      
      return 
      end
      