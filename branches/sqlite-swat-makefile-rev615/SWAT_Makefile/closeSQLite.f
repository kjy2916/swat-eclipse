      subroutine closeSQLite

      use parm

      !!SQLite commit transaction and close database
      call sql_commit(sqliteHandle)
      call close_database(sqliteHandle)

      return

      end
