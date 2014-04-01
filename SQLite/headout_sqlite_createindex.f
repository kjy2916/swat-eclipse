      subroutine headout_sqlite_createindex(indexname,tblname,indexcols)

!!     ~ ~ ~ PURPOSE ~ ~ ~
!!     create index for given table on given columns
!!     The time columns would be added automatically

      use parm

      character(len=*), intent(in)           :: indexname
      character(len=*), intent(in)           :: tblname
      character(len=*), intent(in)           :: indexcols

      character(len=10+size(indecols)        :: indexs

      !!get all index columns
      if(iprint == 2) write(indexs,*) indexcols,',','YR'
      if(iprint == 1) write(indexs,*) indexcols,',','YR,MO,DA'
      if(iprint == 0) write(indexs,*) indexcols,',','YR,MO'

      !!create the index if not existing
      call sqlite3_create_index( db, indexname, tblname,indexs)

      end
