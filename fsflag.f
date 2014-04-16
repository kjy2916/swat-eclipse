      subroutine fsflag
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    this subroutine set global flag to judge whether frozen soil condition
!!    is required, if so, which layer(s) is used. May 10 2011. 
!!    this subroutine must be called after call solt
!!    added by Hailiang Shen

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition  
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    ihru        |none          |current computing HRU
!!    fs_code     |none          |global switch for frozen soil
!!    sol_tmp(:,:)|oC            |soil temperature of different layers in
!!                               |current HRU 
!!    sol_nly(:)  |none          |number of soil layers in each HRU

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    fs_flag     |none          |0: no frozen soil condition adaption
!!                               |1: use the 1st layer info
!!                               |2: use the 2nd layer info


!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~      
      use parm
      
      if (fs_code == 0) then 
        fs_flag = 0
        return 
      end if
      
      if (fs_code == 1) then 
         if (sol_tmp(1, ihru) < 0) fs_flag(ihru) = 1
        
        if (sol_tmp(1, ihru) >= 0) then 
          if (sol_nly(ihru) > 1 .and. sol_tmp(2, ihru) < 0)             &
     &                                                 fs_flag(ihru) = 2
          if (sol_nly(ihru) > 1 .and. sol_tmp(2, ihru) >= 0)             &
     &                                                 fs_flag(ihru) = 0
        end if
      end if
      
      return 
      end