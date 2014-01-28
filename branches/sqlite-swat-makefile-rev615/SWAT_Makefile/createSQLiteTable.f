      subroutine createSQLiteTable(tablename,insertString)

      !!create sqlite result table using given table name
      !!right now the structure of all result talbes are same.
      !!it also genenate the insert string prefix

      use parm

      character(len=3), intent(in) :: tablename
      character(len=1000) :: insertString
      character*1000 columns, columns_types

      columns = "(id,year,water,sediment,PP,DP,TP,PN,DN,TN) values ("
      columns_types =
     &      "(id INTEGER,year INTEGER, water REAL,sediment REAL,"
     &      //"PP REAL,DP REAL,TP REAL,"
     &      //"PN REAL,DN REAL,TN REAL);"

      sqlcmd = 'drop table if exists ' // tablename
      call sql_do(sqliteHandle,sqlcmd_overlay)

      sqlcmd = 'create table if not exists ' // tablename // ' '
     &          // columns_types(:len_trim(columns_types))

      insertString = 'insert into  ' // tablename // ' '
     &          // columns(:len_trim(columns))

      call sql_do(sqliteHandle,sqlcmd_overlay)

!       if(output_test_information) then
!          write(2011,*) sqlcmd
!          write(2011,*) insertString
!       end if
      end
