      subroutine readfs
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine read frozensoil.txt file for frozen soil condition adaption

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    fs_code     |none          |frozen soil code for entire watershed
!!    usle_kc     |none          |usle_k correction factor
!!    erorgn_r    |none          |replacement of erorgn
!!    erorgp_r    |none          |replacement of erorgp
!!    phoskd_r    |none          |revised P soil partitioning coefficient 
!!                               |under thawing condition
!!    nperco_r    |none          |revised N percolation coefficient under 
!!    usle_pr     |none          |revised usle_p for no-till !yongbo
!!    pperco_r    |none          |revised pperco fpor no-till             !yongbo
!!    psp_r       |none          |revised psp for no-till   !yongbo
!!    rsdco_r     |none          |revised rsdco for no-till
!!    p_updis_r   |none          |revised p_updis for no-till
!!    sol_solp_r  |none          |revised sol_solp for no-till
!!    n_updis_r   |nono          |revised n_updis for no-till

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~
      use parm
      
      open (100, file = 'frozensoil.txt')
      read (100, *) fs_codeclose
      read (100, *) usle_kc
      read (100, *) erorgn_r
      read (100, *) erorgp_r
      read (100, *) phoskd_r
      read (100, *) nperco_r
      read (100, *) usle_pr
      read (100, *) pperco_r
      read (100, *) psp_r
      read (100, *) rsdco_r
      read (100, *) p_updis_r
      read (100, *) sol_solp_r
      read (100, *) n_updis_r
      close (100)      
      
      if (fs_codeclose == 0) then
        write (*,*) "Frozen soil is off."
      else
        write (*,*) "Frozen soil is on."
      end if

      return 
      end
      
