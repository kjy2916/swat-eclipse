      subroutine readres2
      
!!    ~ ~ ~ PURPOSE ~ ~ ~
!!    the purpose of this subroutine is to read in data from the reservoir
!!    input file (.re2)

!!    ~ ~ ~ INCOMING VARIABLES ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    i           |none          |reservoir number
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~
!!    name         |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    reslstag(i)                 ! 0 read next number for the volume to be lost annually
!!                                ! 1 use the predicted sediment deposition to adjust the rating curve
!!    reslsv(i)      m3/year      ! average annual deposition rate 
!!    resv(ires,i)                  volume
!!    resa(ires,i)					area
!!    resq(ires,i)					flow
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ LOCAL DEFINITIONS ~ ~ ~
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
!!    titldum     |NA            |title line in .res file (not used in program)
!!    ires                        counter 
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ SUBROUTINES/FUNCTIONS CALLED ~ ~ ~
!!    SWAT: readlwq, caps


!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~

      use parm

      character (len=80) :: titldum
      integer :: ires, deadkey


!!    read in data from .re2 file
 
      read (140,1000) titldum
      read (140,*) reslstag(i)
      read (140,*) reslsv(i)
      deadkey=1
	do ires=1, mpoint(i)
        read (140,*) resv(ires,i),resa(ires,i),resq(ires,i)
        if (deadkey==1.and.resq(ires,i)>0.001.and.ires>1) then
         resdead(i)=resv(ires-1,i)
         deadkey=0
         end if
      end do

!liu temp>
!       write (2222,*)i,resfname(i),resdead(i)   
!       do ires=1,mpoint(i)
!        write (2222,*)resv(ires,i),resa(ires,i),resq(ires,i)
!        end do
!liu temp>


      close (140)

      return
 1000 format (a80)
      end
