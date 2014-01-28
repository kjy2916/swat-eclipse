/* 
   Wrapper routines that Fortran code can use to call SQLite library
   functions.
   http://danial.org/sqlite/fortran/      A. Danial   July 2005
*/ 
#include "sqlite3.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/time.h>

int sql_begin_(sqlite3 **fortran_dbh) { /* {{{1 */
    char *errmsg;
    sqlite3 *db;
    db = *fortran_dbh;
    if (sqlite3_exec(db, "BEGIN TRANSACTION",
                     NULL, NULL, &errmsg) != SQLITE_OK) {
        printf("couldn't begin transaction:  %s\n", errmsg);
        return 0;
    }
    return 1;
} /* 1}}} */
int sql_commit_(sqlite3 **fortran_dbh) { /* {{{1 */
    char *errmsg;
    sqlite3 *db;
    db = *fortran_dbh;
    if (sqlite3_exec(db, "COMMIT TRANSACTION",
                     NULL, NULL, &errmsg) != SQLITE_OK) {
        printf("couldn't commit transaction:  %s\n", errmsg);
        return 0;
    } else {
        return 1;
    }
} /* 1}}} */
float elapsed(struct timeval start, struct timeval end) { /* {{{1 */
    return  (float) (end.tv_sec  - start.tv_sec ) +
           ((float) (end.tv_usec - start.tv_usec)/1000000);
} /* 1}}} */
int sql_insert_(sqlite3 **fortran_dbh, /* {{{1 */
               char     *table      , /* in string, table name              */
               int      *nrows      , /* in scalar, number of data rows     */
               int      *nfields    , /* in scalar, number of data columns  */
               int      *type       , /* in array, type of each column      */
               int      *nwords     , /* in array, # words each column      */
               int      *data       ) /* in array, data payload             */
{
int DBG = 0;
/*
      type : 1=int  2=float  3=double  4=char*4
      size : number of words taken by this field
 
      Inserts nrows of data in to the specified table in a single
      transaction.  The data may be of mixed type.  For example if
      the table has five columns with types
          integer,  char(20),  float, double, integer
      then
          *nrows = 5
          type[0] = 1      nWords[0] = 1
          type[1] = 4      nWords[1] = 5
          type[2] = 2      nWords[2] = 2
          type[3] = 3      nWords[3] = 1
          type[4] = 1      nWords[4] = 1
                                   
          row_words = sum(nwords[0..4]) = 1+5+2+1+1 = 10 words
          Data for first row:
            data[   0 + row_words*0] = integer overlay
            data[1..5 + row_words*0] = 20 character overlay
            data[   6 + row_words*0] = integer overlay
            data[7..8 + row_words*0] = double  overlay
            data[   9 + row_words*0] = integer overlay
          Data for second row:
            data[   0 + row_words*1] = integer overlay
            data[1..5 + row_words*1] = 20 character overlay
            data[   6 + row_words*1] = integer overlay
            data[7..8 + row_words*1] = double  overlay
            data[   9 + row_words*1] = integer overlay
          et cetera
*/              
    sqlite3_stmt *Stmt;
    const char *zLeftover;
    int    rc, i, j, offset, len, row_words;

    char   *c_data;
    float  *f_data;
    double *d_data;

    float  delta_T;
#define MAX_COMMAND 2000
    char   insert_command[MAX_COMMAND-1];
    struct timeval start_time, end_time;
    sqlite3 *db;

if (DBG) printf("sql_insert nrows=%d  nfields=%d  T=[%s]\n", 
*nrows, *nfields, table); 
if (DBG) printf("sql_insert data[0] = %d\n", data[0]);

    gettimeofday(&start_time, 0);
    sql_begin_(fortran_dbh);

    db = *fortran_dbh;
    snprintf(insert_command, MAX_COMMAND, "insert into %s values(", table);
    if (strlen(insert_command) > MAX_COMMAND - 2*(*nfields) - 5) {
        /* table name is too long, would be buffer overflow */
        return 0;
    }
    row_words = 0;
    for (j = 0; j < *nfields; j++) {
        row_words += nwords[j];
        strcat(insert_command, "?");
        if (j < (*nfields-1)) {
            strcat(insert_command, ",");
        }
    }
    strcat(insert_command, ");");
if (DBG) printf("sql_insert insert comm=[%s]\n", insert_command);

    rc = sqlite3_prepare(db, insert_command, -1, &Stmt, &zLeftover);

    for (i = 0; i < *nrows; i++) {
        offset = 0;
        for (j = 0; j < *nfields; j++) {

if (DBG) printf("sql_insert switch loop i=%d  j=%d  off=%2d  type=%d   data[%3d]\n", i,j,offset,type[j], offset + row_words*i);

            switch (type[j]) {
                case 1: /* integer */
if (DBG) printf("%11d|",       data[offset + row_words*i]);
                        rc = sqlite3_bind_int(   Stmt, j+1, data[offset + row_words*i]);
                        break;
                case 2: /* real*4  */
                        f_data = (float *)  &(data[offset + row_words*i]);
if (DBG) printf("% 10.6e|",  *f_data);
                        rc = sqlite3_bind_double(Stmt, j+1, *f_data);
                        break;
                case 3: /* real*8  */
                        d_data = (double *) &(data[offset + row_words*i]);
if (DBG) printf("% 12.8le|", *d_data);
                        rc = sqlite3_bind_double(Stmt, j+1, (float) *d_data);
                        break;
                case 4: /* char    */
                        c_data = (char *)   &(data[offset + row_words*i]);
                        len    = strlen(c_data);
                        rc     = sqlite3_bind_text(Stmt, j+1, c_data, len, SQLITE_STATIC);
if (DBG) printf("%s|",       c_data);
                        break;
                default: /* error  */
                        printf("sql_insert ERROR row %d col %d type=%d", 
                               i+1, j+1, type[i]);
                        break;
            }
            offset += nwords[j];
        }
if (DBG) printf("\n");
        rc = sqlite3_step(Stmt);
        rc = sqlite3_reset(Stmt);
    }
    sql_commit_(fortran_dbh);

    gettimeofday(&end_time, 0);
    delta_T = elapsed(start_time, end_time);
    printf(" %d inserts in %.3f s = %.2f inserts/s\n",
            *nrows, delta_T, *nrows/delta_T);

    return 1;
} /* 1}}} */
int sql_do_(sqlite3 **fortran_dbh, /* {{{1 */
           char     *command    ) /* in string, SQL statement           */
{
    sqlite3_stmt *Stmt;
    const char *zLeftover;
    int    rc = 0;
    sqlite3 *db;

int DBG = 0;
    db = *fortran_dbh;

if (DBG) printf("sql_do [%s]\n", command);

    rc = sqlite3_prepare(db, command, -1, &Stmt, &zLeftover);
if (DBG) printf("sql_do prepare rc=%d\n", rc);
    if (rc != SQLITE_OK) return rc;

    rc = sqlite3_step(Stmt);
if (DBG) printf("sql_do step rc=%d\n", rc);
    if ((rc != SQLITE_OK) && (rc != SQLITE_DONE)) return rc;

    rc = sqlite3_finalize(Stmt);
if (DBG) printf("sql_do finalize rc=%d\n", rc);

    return rc;
} /* 1}}} */
int sql_query_(sqlite3 **fortran_dbh, /* {{{1 */
              char     *query      , /* in                                 */
              int      *nCols      , /* in  number of columns in result    */
              int      *col_type   , /* in  array of data types each col   */
              int      *nRows      , /* out number of rows found           */
              int      *results)     /* out data buffer containing results */ 
{
    int           rc = -1, word_offset = 0, col;
    int          *i_data;
    float        *f_data;
    double       *d_data;
    sqlite3_stmt *RowData;
    sqlite3      *db;

int DBG = 0;
    db = *fortran_dbh;

    *nRows = 0;

    rc = sqlite3_prepare(db, query, -1, &RowData, 0);
    if (rc != SQLITE_OK) {
        printf("sql_query: prepare failed for '%s'\n", query);
        return rc;
    }

    /* Loop through the rows and extract the results. */
    while ((rc = sqlite3_step(RowData)) == SQLITE_ROW) {

        for (col = 0; col < *nCols; ++col) {
            switch ( col_type[col] ) {
            /* type : 1=int  2=float  3=double  4=char*4 */
            case 1:
                i_data =         &results[word_offset];
               *i_data = sqlite3_column_int(RowData, col);
if (DBG) printf("sql_query row %d col %d:  i=%d\n", *nRows, col+1, *i_data);
                word_offset += 1;
                break;
            case 2:
                f_data = (float *) &results[word_offset];
               *f_data = sqlite3_column_double(RowData,col);
if (DBG) printf("sql_query row %d col %d:  f=%e\n", *nRows, col+1, *f_data);
                word_offset += 1;
                break;
            case 3:
                d_data = (double *) &results[word_offset];
               *d_data = sqlite3_column_double(RowData,col);
if (DBG) printf("sql_query row %d col %d:  d=%e\n", *nRows, col+1, *d_data);
                word_offset += 2;
                break;
            case 4:
                strcpy((char *) &results[word_offset],  
                       sqlite3_column_text(RowData, col));
                       /* beware buffer overflow */
                word_offset += 10;
                break;
            default:
                printf("sql_query:  don't understand data type %d for column %d\n", 
                        col_type[col], col+1);
                return -1;
            }
        }
if (DBG) printf("\n");
        ++(*nRows);
    }
    return sqlite3_finalize(RowData);
} /* 1}}} */
int open_database_(char *filename, int *fortran_dbh) { /* {{{1 */
    sqlite3 *db;
    int rc;

    rc = sqlite3_open(filename, &db);
    *fortran_dbh = (int) db;
    
    if (rc != SQLITE_OK) {
        printf("open_database: failed to open %s, rc=%d\n", filename, rc);
        return 0;
    }
    return 1;
} /* 1}}} */
int close_database_(sqlite3 **fortran_dbh) { /* {{{1 */
    sqlite3 *db;
    db = *fortran_dbh;
    sqlite3_close(db);
    return 1;
} /* 1}}} */
