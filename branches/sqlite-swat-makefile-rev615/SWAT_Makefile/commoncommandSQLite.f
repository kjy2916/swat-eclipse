      subroutine commoncommandSQLite(insertString,id,year,
     &                          water,sediment,
     &                          PP,DP,TP,
     &                          PN,DN,TN)

      !!common SQLite sqlcmd for all five result table
      !!hru,subbasin,reach,reservoir,recday(point source)
      !!the tables are same for all the table
      !!unit of water is different for them
      !!HRU:        mm
      !!Subbasin:   mm
      !!reach:      m^3/s(cms)
      !!reservoir:  m^3/s
      !!recday:     m^3
      !!unit of sediment is metric tons
      !!unit of N/P is kg
      use parm

      character*1000, intent(in) :: insertString
      integer, intent(in) :: id,year
      real, intent(in) :: water,sediment,PP,DP,TP,PN,DN,TN

      character (len=10) :: sqlitedata

       sqlcmd = insertString

       !!hru id
       write(sqlitedata,"(i10)") id
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) //sqlitedata

       !!current day of simulation
       write(sqlitedata,"(i10)") year
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!water
       write(sqlitedata,"(e10.3)") water
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!sediment
       write(sqlitedata,"(e10.3)") sediment
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!PP
       write(sqlitedata,"(e10.3)") PP
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!PP
       write(sqlitedata,"(e10.3)") DP
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!TP
       write(sqlitedata,"(e10.3)") TP
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!PN
       write(sqlitedata,"(e10.3)") PN
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!PN
       write(sqlitedata,"(e10.3)") DN
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       !!TN
       write(sqlitedata,"(e10.3)") TN
       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ',' // sqlitedata

       sqlcmd = sqlcmd(:len_trim(sqlcmd)) // ');'

!       if(output_test_information) then
!          write(2011,*) sqlcmd
!       end if

      call sql_do(sqliteHandle,sqlcmd_overlay)

      end
