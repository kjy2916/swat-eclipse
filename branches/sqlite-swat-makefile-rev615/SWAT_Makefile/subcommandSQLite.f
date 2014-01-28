      subroutine subcommandSQLite(sub_id,period,all_result,part_result)

      use parm

      integer, intent(in) :: sub_id
      real, intent(in), dimension(msubo) :: all_result
      real, intent(in), dimension(msubo) :: part_result
      integer, intent(in) :: period !!maybe day,month,year
      integer :: j
      character (len=10) :: sqlitedata

      j = sub_id

      !!generate the insert sql for one hru result to save in SQLite
      !!Zhiqiang, 2012-9-25
       sqlcmd = subinsert

       !!sub id
       write(sqlitedata,"(i10)") j
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // sqlitedata

       !!current day of simulation
       write(sqlitedata,"(i10)") period
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       do ii = 1, itotb
          write(sqlitedata,"(e10.3)") part_result(ii)
          sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata
       end do

       !!PP = ORGP + SEDP
       write(sqlitedata,"(e10.3)") part_result(12) + part_result(15)
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!TDN = NSURQ + LATNO3 + GWNO3
       write(sqlitedata,"(e10.3)")
     &   part_result(13) + part_result(17) + part_result(18)
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!TP = ORGP + SOLP + SEDP
       write(sqlitedata,"(e10.3)")
     &   part_result(12) + part_result(14) + part_result(15)
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!TN = ORGN + NSURQ + LATNO3 + GWNO3
       write(sqlitedata,"(e10.3)")
     &   part_result(13) + part_result(17) + part_result(18)
     &   + part_result(11)
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

      sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ');'

!       if(output_test_information) then
!          write(2011,*) sqlcmd
!       end if

      call sql_do(sqliteHandle,sqlcmd_overlay)



      end
