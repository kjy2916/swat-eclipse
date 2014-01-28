      subroutine rsvcommandSQLite(rsv_id,period,part_result)

      use parm

      integer, intent(in) :: rsv_id
      real, intent(in), dimension(41) :: part_result
      integer, intent(in) :: period !!maybe day,month,year
      integer :: j
      character (len=10) :: sqlitedata

      j = rsv_id

      !!generate the insert sql for one hru result to save in SQLite
      !!Zhiqiang, 2012-9-25
       sqlcmd = rsvinsert

       !!reach id
       write(sqlitedata,"(i10)") j
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // sqlitedata

       !!current day of simulation
       write(sqlitedata,"(i10)") period
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       do ii = 1, itotreservoir
          write(sqlitedata,"(e10.3)") part_result(ii)
          sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata
       end do


       !!TDN = NO3 + NH3 + NO2
       write(sqlitedata,"(e10.3)")
     &   part_result(9) + part_result(10) + part_result(11)
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!TN = ORGN + NO3 + NH4 + NO2
       write(sqlitedata,"(e10.3)")
     &   part_result(7) + part_result(9) + part_result(10)
     &   + part_result(11)
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!TP = ORGP + MINP
       write(sqlitedata,"(e10.3)")
     &   part_result(8) + part_result(12)
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata


      sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ');'

!       if(output_test_information) then
!          write(2011,*) sqlcmd
!       end if

      call sql_do(sqliteHandle,sqlcmd_overlay)



      end
