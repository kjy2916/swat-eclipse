      subroutine mgt_sqlite(id_op)

        use parm

        integer,intent(in) :: id_op
        j = ihru

        if(imgt == 0) return;

        if(id_op == 10) return;
        if(id_op == 11) return;
        if(id_op == 14) return;
        if(id_op == 15) return;

        call sqlite3_set_column( colmgt(1), hruno(j) )
        call sqlite3_set_column( colmgt(2), j )
        call sqlite3_set_column( colmgt(3), iyr )
        call sqlite3_set_column( colmgt(4), i_mo )
        call sqlite3_set_column( colmgt(5), icl(iida) )
        call sqlite3_set_column( colmgt(6), "" )
        !call sqlite3_set_column( colmgt(7), soperation )

        call sqlite3_set_column( colmgt(8), phubase(j) )
        call sqlite3_set_column( colmgt(9), phuacc(j) )
        call sqlite3_set_column( colmgt(10), sol_sw(j) )
        call sqlite3_set_column( colmgt(11), bio_ms(j) )
        call sqlite3_set_column( colmgt(12), sol_rsd(1,j) )
        call sqlite3_set_column( colmgt(13), sol_sumno3(j) )
        call sqlite3_set_column( colmgt(14), sol_sumsolp(j) )
        call sqlite3_set_column( colmgt(15), 0.0 ) !!yield for harvest/kill
        call sqlite3_set_column( colmgt(16), 0.0 ) !!mix efficiency

        call sqlite3_set_column( colmgt(17), 0.0 ) !!6 fertilizer
        call sqlite3_set_column( colmgt(18), 0.0 )
        call sqlite3_set_column( colmgt(19), 0.0 )
        call sqlite3_set_column( colmgt(20), 0.0 )
        call sqlite3_set_column( colmgt(21), 0.0 )
        call sqlite3_set_column( colmgt(22), 0.0 )

        call sqlite3_set_column( colmgt(23), 0.0 ) !!pesticide

        call sqlite3_set_column( colmgt(24), 0.0 ) !!5 stress for harvest/kill
        call sqlite3_set_column( colmgt(25), 0.0 )
        call sqlite3_set_column( colmgt(26), 0.0 )
        call sqlite3_set_column( colmgt(27), 0.0 )
        call sqlite3_set_column( colmgt(28), 0.0 )

        call sqlite3_set_column( colmgt(29), 0.0 ) !!Harvest yield
        call sqlite3_set_column( colmgt(30), 0.0 )
        call sqlite3_set_column( colmgt(31), 0.0 )
        call sqlite3_set_column( colmgt(32), 0.0 )
        call sqlite3_set_column( colmgt(33), 0.0 )
        call sqlite3_set_column( colmgt(34), 0.0 )

        call sqlite3_set_column( colmgt(35), 0.0 ) !!manure

        call sqlite3_set_column( colmgt(36), 0.0 ) !!irrigation
        call sqlite3_set_column( colmgt(37), 0 )
        call sqlite3_set_column( colmgt(38), 0 )
        select case (id_op)
            case (1)
                call sqlite3_set_column( colmgt(6), cpnm(idplt(j)) )
                call sqlite3_set_column( colmgt(7), "PLANT" )
            case (2) !!irrigation
                call sqlite3_set_column( colmgt(7), "IRRIGATE" )
                call sqlite3_set_column( colmgt(37), irr_sc(j) )
                call sqlite3_set_column( colmgt(38), irr_no(j) )
            case (3) !!fertilizer
                call sqlite3_set_column( colmgt(6), fertnm(ifrttyp) )
                call sqlite3_set_column( colmgt(7), "FERT" )
                call sqlite3_set_column( colmgt(17), frt_kg )
                call sqlite3_set_column( colmgt(18), fertno3 )
                call sqlite3_set_column( colmgt(19), fertnh3 )
                call sqlite3_set_column( colmgt(20), fertorgn )
                call sqlite3_set_column( colmgt(21), fertsolp )
                call sqlite3_set_column( colmgt(22), fertorgp )
            case (4) !!pesticide
                call sqlite3_set_column( colmgt(6), pname(ipest) )
                call sqlite3_set_column( colmgt(7), "PEST" )
                call sqlite3_set_column( colmgt(23), pst_kg )
            case (5) !! harvest and kill operation
                call sqlite3_set_column( colmgt(6), cpnm(idplt(j)) )
                call sqlite3_set_column( colmgt(7), "HARV/KILL" )
                call sqlite3_set_column( colmgt(15), yield )
                call sqlite3_set_column( colmgt(24), strsn_sum(j) ) !!5 stress for harvest/kill
                call sqlite3_set_column( colmgt(25), strsp_sum(j) )
                call sqlite3_set_column( colmgt(26), strstmp_sum(j) )
                call sqlite3_set_column( colmgt(27), strsw_sum(j) )
                call sqlite3_set_column( colmgt(28), strsa_sum(j) )
            case (6) !!tillage
                call sqlite3_set_column( colmgt(6), tillnm(idtill) )
                call sqlite3_set_column( colmgt(7), "TILLAGE" )
                call sqlite3_set_column( colmgt(16), effmix(idtill) )
            case (7) !!Harvest only
                call sqlite3_set_column( colmgt(6), cpnm(idplt(j)) )
                call sqlite3_set_column( colmgt(7), "HARVEST ONLY" )
                call sqlite3_set_column( colmgt(15), yield )

                call sqlite3_set_column( colmgt(24), strsn_sum(j) ) !!5 stress for harvest/kill
                call sqlite3_set_column( colmgt(25), strsp_sum(j) )
                call sqlite3_set_column( colmgt(26), strstmp_sum(j) )
                call sqlite3_set_column( colmgt(27), strsw_sum(j) )
                call sqlite3_set_column( colmgt(28), strsa_sum(j) )

                call sqlite3_set_column( colmgt(29), yieldgrn )
                call sqlite3_set_column( colmgt(30), yieldbms )
                call sqlite3_set_column( colmgt(31), yieldtbr )
                call sqlite3_set_column( colmgt(32), yieldrsd )
                call sqlite3_set_column( colmgt(33), yieldn )
                call sqlite3_set_column( colmgt(34), yieldp )
            case (8) !!kill
                call sqlite3_set_column( colmgt(7), "KILL" )
            case (9) !!grazing
                call sqlite3_set_column( colmgt(7), "GRAZE" )
                call sqlite3_set_column( colmgt(35), manure_kg(j))
            case (10) !!auto irrigation
            case (11) !!auto fertilizer
            case (12) !!street sweeping (only if iurban=2)
                call sqlite3_set_column( colmgt(7), "STREET SWEEP" )
            case (13) !!release/impound water in rice fields
                call sqlite3_set_column( colmgt(7), "RELEASE/IMPOUND" )
            case (14) !!continuous fertilization
            case (15) !!continuous pesticide
            case (16) !!burning
                call sqlite3_set_column( colmgt(7), "BURN" )
            !!following codes are not real code
            case (17) !!start-dorm
                call sqlite3_set_column( colmgt(6), cpnm(idplt(j)) )
                call sqlite3_set_column( colmgt(7), "START-DORM" )
            case (18) !!end-dorm
                call sqlite3_set_column( colmgt(6), cpnm(idplt(j)) )
                call sqlite3_set_column( colmgt(7), "END-DORM" )
            case (19) !!continuous fertilization
                call sqlite3_set_column( colmgt(17), cfrt_kg(j) )
                call sqlite3_set_column( colmgt(7), "CONT FERT" )
            case (20) !!continuous pesticide
                call sqlite3_set_column( colmgt(23), cpst_kg(j) )
                call sqlite3_set_column( colmgt(7), "CONT PEST" )
            case (21) !!auto irrigation
                call sqlite3_set_column( colmgt(7), "AUTOIRR" )
                call sqlite3_set_column( colmgt(36), aird(j) )
                call sqlite3_set_column( colmgt(37), irrsc(j) )
                call sqlite3_set_column( colmgt(38), irrno(j) )
            case (22) !!auto fertilizer
                call sqlite3_set_column( colmgt(7), "AUTOFERT" )
                call sqlite3_set_column( colmgt(17), frt_kg )
                call sqlite3_set_column( colmgt(18), fertno3 )
                call sqlite3_set_column( colmgt(19), fertnh3 )
                call sqlite3_set_column( colmgt(20), fertorgn )
                call sqlite3_set_column( colmgt(21), fertsolp )
                call sqlite3_set_column( colmgt(22), fertorgp )
        end select

        call sqlite3_insert( db, tblmgt, colmgt )

      end
