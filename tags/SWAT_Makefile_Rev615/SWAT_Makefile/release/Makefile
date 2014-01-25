FC=gfortran
NAME=SWAT
OBJECTS=./modparm.o ./addh.o ./albedo.o ./allocate_parms.o ./alph.o ./anfert.o ./apex_day.o ./apply.o ./ascrv.o ./atri.o ./aunif.o ./autoirr.o ./aveval.o ./bacteria.o ./biofilm.o ./biozone.o ./bmpfixed.o ./bmpinit.o ./bmp_det_pond.o ./bmp_ri_pond.o ./bmp_sand_filter.o ./bmp_sed_pond.o ./bmp_wet_pond.o ./buffer.o ./burnop.o ./canopyint.o ./caps.o ./carbon_new.o ./carbon_zhang2.o ./cfactor.o ./chkcst.o ./clgen.o ./clicon.o ./command.o ./conapply.o ./confert.o ./crackflow.o ./crackvol.o ./curno.o ./dailycn.o ./decay.o ./depstor.o ./distrib_bmps.o ./dormant.o ./drains.o ./dstn1.o ./ee.o ./eiusle.o ./enrsb.o ./erfc.o ./estimate_ksat.o ./etact.o ./etpot.o ./expo.o ./fert.o ./filter.o ./filtw.o ./finalbal.o ./gcycl.o ./getallo.o ./grass_wway.o ./graze.o ./grow.o ./gwmod.o ./gwmod_deep.o ./gwnutr.o ./gw_no3.o ./h2omgt_init.o ./harvestop.o ./harvgrainop.o ./harvkillop.o ./header.o ./headout.o ./hhnoqual.o ./hhwatqual.o ./hmeas.o ./hruaa.o ./hruallo.o ./hruday.o ./hrumon.o ./hrupond.o ./hrupondhr.o ./hruyr.o ./hydroinit.o ./icl.o ./impndaa.o ./impndday.o ./impndmon.o ./impndyr.o ./impnd_init.o ./irrigate.o ./irrsub.o ./irr_rch.o ./irr_res.o ./jdt.o ./killop.o ./lakeq.o ./latsed.o ./layersplit.o ./lwqdef.o ./main.o ./ncsed_leach.o ./ndenit.o ./newtillmix.o ./nfix.o ./nitvol.o ./nlch.o ./nminrl.o ./noqual.o ./npup.o ./nrain.o ./nup.o ./nuts.o ./openwth.o ./operatn.o ./orgn.o ./orgncswat.o ./origtile.o ./ovr_sed.o ./percmacro.o ./percmain.o ./percmicro.o ./pestlch.o ./pestw.o ./pesty.o ./pgen.o ./pgenhr.o ./pkq.o ./plantmod.o ./plantop.o ./pmeas.o ./pminrl.o ./pminrl2.o ./pond.o ./pondhr.o ./pothole.o ./potholehr.o ./print_hyd.o ./psed.o ./qman.o ./ran1.o ./rchaa.o ./rchday.o ./rchinit.o ./rchmon.o ./rchuse.o ./rchyr.o ./reachout.o ./readatmodep.o ./readbsn.o ./readchm.o ./readcnst.o ./readfcst.o ./readfert.o ./readfig.o ./readfile.o ./readgw.o ./readhru.o ./readinpt.o ./readlup.o ./readlwq.o ./readmgt.o ./readmon.o ./readops.o ./readpest.o ./readplant.o ./readpnd.o ./readres.o ./readrte.o ./readru.o ./readsdr.o ./readsepticbz.o ./readseptwq.o ./readsno.o ./readsol.o ./readsub.o ./readswq.o ./readtill.o ./readurban.o ./readwgn.o ./readwus.o ./readwwq.o ./readyr.o ./reccnst.o ./recday.o ./rechour.o ./recmon.o ./recyear.o ./regres.o ./res.o ./resbact.o ./resetlu.o ./reshr.o ./resinit.o ./resnut.o ./rewind_init.o ./rhgen.o ./rootfr.o ./route.o ./routels.o ./routeunit.o ./routres.o ./rsedaa.o ./rseday.o ./rsedmon.o ./rsedyr.o ./rtbact.o ./rtday.o ./rteinit.o ./rthmusk.o ./rthpest.o ./rthr.o ./rthsed.o ./rtmusk.o ./rtout.o ./rtpest.o ./rtsed.o ./rtsed_bagnold.o ./rtsed_kodatie.o ./rtsed_molinas_wu.o ./rtsed_yangsand.o ./sat_excess.o ./save.o ./saveconc.o ./schedule_ops.o ./sched_mgt.o ./simulate.o ./sim_initday.o ./sim_inityr.o ./slrgen.o ./smeas.o ./snom.o ./soil_chem.o ./soil_par.o ./soil_phys.o ./soil_write.o ./solp.o ./solt.o ./std1.o ./std2.o ./std3.o ./stdaa.o ./storeinitial.o ./structure.o ./subaa.o ./subbasin.o ./subday.o ./submon.o ./substor.o ./subwq.o ./subyr.o ./sub_subbasin.o ./sumhyd.o ./sumv.o ./surface.o ./surfstor.o ./surfst_h2o.o ./surq_daycn.o ./surq_greenampt.o ./swbl.o ./sweep.o ./swu.o ./tair.o ./tgen.o ./theta.o ./tillfactor.o ./tillmix.o ./tmeas.o ./tran.o ./transfer.o ./tstr.o ./ttcoef.o ./ttcoef_wway.o ./urban.o ./urbanhr.o ./urb_bmp.o ./varinit.o ./vbl.o ./virtual.o ./volq.o ./washp.o ./watbal.o ./water_hru.o ./watqual.o ./watqual2.o ./wattable.o ./watuse.o ./weatgn.o ./wetlan.o ./wmeas.o ./wndgen.o ./writea.o ./writeaa.o ./writed.o ./writem.o ./xmon.o ./ysed.o ./zero0.o ./zero1.o ./zero2.o ./zeroini.o ./zero_urbn.o

all: ${NAME}

${NAME}: ${OBJECTS}
	${FC} ${OBJECTS} -static -o ${NAME}

./modparm.o: ../modparm.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow -ffixed-line-length-132 ../modparm.f -o ./modparm.o 

./addh.o: ../addh.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../addh.f -o ./addh.o 

./albedo.o: ../albedo.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../albedo.f -o ./albedo.o 

./allocate_parms.o: ../allocate_parms.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../allocate_parms.f -o ./allocate_parms.o 

./alph.o: ../alph.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../alph.f -o ./alph.o 

./anfert.o: ../anfert.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../anfert.f -o ./anfert.o 

./apex_day.o: ../apex_day.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../apex_day.f -o ./apex_day.o 

./apply.o: ../apply.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../apply.f -o ./apply.o 

./ascrv.o: ../ascrv.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../ascrv.f -o ./ascrv.o 

./atri.o: ../atri.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../atri.f -o ./atri.o 

./aunif.o: ../aunif.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../aunif.f -o ./aunif.o 

./autoirr.o: ../autoirr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../autoirr.f -o ./autoirr.o 

./aveval.o: ../aveval.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../aveval.f -o ./aveval.o 

./bacteria.o: ../bacteria.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../bacteria.f -o ./bacteria.o 

./biofilm.o: ../biofilm.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../biofilm.f -o ./biofilm.o 

./biozone.o: ../biozone.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow -ffixed-line-length-132 ../biozone.f -o ./biozone.o 

./bmpfixed.o: ../bmpfixed.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../bmpfixed.f -o ./bmpfixed.o 

./bmpinit.o: ../bmpinit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow -ffixed-line-length-132 ../bmpinit.f -o ./bmpinit.o 

./bmp_det_pond.o: ../bmp_det_pond.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../bmp_det_pond.f -o ./bmp_det_pond.o 

./bmp_ri_pond.o: ../bmp_ri_pond.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../bmp_ri_pond.f -o ./bmp_ri_pond.o 

./bmp_sand_filter.o: ../bmp_sand_filter.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../bmp_sand_filter.f -o ./bmp_sand_filter.o 

./bmp_sed_pond.o: ../bmp_sed_pond.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../bmp_sed_pond.f -o ./bmp_sed_pond.o 

./bmp_wet_pond.o: ../bmp_wet_pond.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../bmp_wet_pond.f -o ./bmp_wet_pond.o 

./buffer.o: ../buffer.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../buffer.f -o ./buffer.o 

./burnop.o: ../burnop.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../burnop.f -o ./burnop.o 

./canopyint.o: ../canopyint.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../canopyint.f -o ./canopyint.o 

./caps.o: ../caps.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../caps.f -o ./caps.o 

./carbon_new.o: ../carbon_new.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../carbon_new.f -o ./carbon_new.o 

./carbon_zhang2.o: ../carbon_zhang2.f90
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow -ffree-line-length-200 ../carbon_zhang2.f90 -o ./carbon_zhang2.o 

./cfactor.o: ../cfactor.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../cfactor.f -o ./cfactor.o 

./chkcst.o: ../chkcst.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../chkcst.f -o ./chkcst.o 

./clgen.o: ../clgen.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../clgen.f -o ./clgen.o 

./clicon.o: ../clicon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../clicon.f -o ./clicon.o 

./command.o: ../command.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../command.f -o ./command.o 

./conapply.o: ../conapply.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../conapply.f -o ./conapply.o 

./confert.o: ../confert.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../confert.f -o ./confert.o 

./crackflow.o: ../crackflow.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../crackflow.f -o ./crackflow.o 

./crackvol.o: ../crackvol.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../crackvol.f -o ./crackvol.o 

./curno.o: ../curno.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../curno.f -o ./curno.o 

./dailycn.o: ../dailycn.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../dailycn.f -o ./dailycn.o 

./decay.o: ../decay.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../decay.f -o ./decay.o 

./depstor.o: ../depstor.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../depstor.f -o ./depstor.o 

./distrib_bmps.o: ../distrib_bmps.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../distrib_bmps.f -o ./distrib_bmps.o 

./dormant.o: ../dormant.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../dormant.f -o ./dormant.o 

./drains.o: ../drains.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../drains.f -o ./drains.o 

./dstn1.o: ../dstn1.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../dstn1.f -o ./dstn1.o 

./ee.o: ../ee.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../ee.f -o ./ee.o 

./eiusle.o: ../eiusle.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../eiusle.f -o ./eiusle.o 

./enrsb.o: ../enrsb.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../enrsb.f -o ./enrsb.o 

./erfc.o: ../erfc.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../erfc.f -o ./erfc.o 

./estimate_ksat.o: ../estimate_ksat.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../estimate_ksat.f -o ./estimate_ksat.o 

./etact.o: ../etact.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../etact.f -o ./etact.o 

./etpot.o: ../etpot.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../etpot.f -o ./etpot.o 

./expo.o: ../expo.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../expo.f -o ./expo.o 

./fert.o: ../fert.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../fert.f -o ./fert.o 

./filter.o: ../filter.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../filter.f -o ./filter.o 

./filtw.o: ../filtw.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../filtw.f -o ./filtw.o 

./finalbal.o: ../finalbal.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../finalbal.f -o ./finalbal.o 

./gcycl.o: ../gcycl.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../gcycl.f -o ./gcycl.o 

./getallo.o: ../getallo.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../getallo.f -o ./getallo.o 

./grass_wway.o: ../grass_wway.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../grass_wway.f -o ./grass_wway.o 

./graze.o: ../graze.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../graze.f -o ./graze.o 

./grow.o: ../grow.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../grow.f -o ./grow.o 

./gwmod.o: ../gwmod.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../gwmod.f -o ./gwmod.o 

./gwmod_deep.o: ../gwmod_deep.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../gwmod_deep.f -o ./gwmod_deep.o 

./gwnutr.o: ../gwnutr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../gwnutr.f -o ./gwnutr.o 

./gw_no3.o: ../gw_no3.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../gw_no3.f -o ./gw_no3.o 

./h2omgt_init.o: ../h2omgt_init.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../h2omgt_init.f -o ./h2omgt_init.o 

./harvestop.o: ../harvestop.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../harvestop.f -o ./harvestop.o 

./harvgrainop.o: ../harvgrainop.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../harvgrainop.f -o ./harvgrainop.o 

./harvkillop.o: ../harvkillop.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../harvkillop.f -o ./harvkillop.o 

./header.o: ../header.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../header.f -o ./header.o 

./headout.o: ../headout.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../headout.f -o ./headout.o 

./hhnoqual.o: ../hhnoqual.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hhnoqual.f -o ./hhnoqual.o 

./hhwatqual.o: ../hhwatqual.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hhwatqual.f -o ./hhwatqual.o 

./hmeas.o: ../hmeas.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hmeas.f -o ./hmeas.o 

./hruaa.o: ../hruaa.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hruaa.f -o ./hruaa.o 

./hruallo.o: ../hruallo.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hruallo.f -o ./hruallo.o 

./hruday.o: ../hruday.f90
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hruday.f90 -o ./hruday.o 

./hrumon.o: ../hrumon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hrumon.f -o ./hrumon.o 

./hrupond.o: ../hrupond.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hrupond.f -o ./hrupond.o 

./hrupondhr.o: ../hrupondhr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hrupondhr.f -o ./hrupondhr.o 

./hruyr.o: ../hruyr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hruyr.f -o ./hruyr.o 

./hydroinit.o: ../hydroinit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../hydroinit.f -o ./hydroinit.o 

./icl.o: ../icl.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../icl.f -o ./icl.o 

./impndaa.o: ../impndaa.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../impndaa.f -o ./impndaa.o 

./impndday.o: ../impndday.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../impndday.f -o ./impndday.o 

./impndmon.o: ../impndmon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../impndmon.f -o ./impndmon.o 

./impndyr.o: ../impndyr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../impndyr.f -o ./impndyr.o 

./impnd_init.o: ../impnd_init.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../impnd_init.f -o ./impnd_init.o 

./irrigate.o: ../irrigate.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../irrigate.f -o ./irrigate.o 

./irrsub.o: ../irrsub.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../irrsub.f -o ./irrsub.o 

./irr_rch.o: ../irr_rch.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../irr_rch.f -o ./irr_rch.o 

./irr_res.o: ../irr_res.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../irr_res.f -o ./irr_res.o 

./jdt.o: ../jdt.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../jdt.f -o ./jdt.o 

./killop.o: ../killop.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../killop.f -o ./killop.o 

./lakeq.o: ../lakeq.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../lakeq.f -o ./lakeq.o 

./latsed.o: ../latsed.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../latsed.f -o ./latsed.o 

./layersplit.o: ../layersplit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../layersplit.f -o ./layersplit.o 

./lwqdef.o: ../lwqdef.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../lwqdef.f -o ./lwqdef.o 

./main.o: ../main.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../main.f -o ./main.o 

./ncsed_leach.o: ../NCsed_leach.f90
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../NCsed_leach.f90 -o ./ncsed_leach.o 

./ndenit.o: ../ndenit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../ndenit.f -o ./ndenit.o 

./newtillmix.o: ../newtillmix.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../newtillmix.f -o ./newtillmix.o 

./nfix.o: ../nfix.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../nfix.f -o ./nfix.o 

./nitvol.o: ../nitvol.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../nitvol.f -o ./nitvol.o 

./nlch.o: ../nlch.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../nlch.f -o ./nlch.o 

./nminrl.o: ../nminrl.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../nminrl.f -o ./nminrl.o 

./noqual.o: ../noqual.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../noqual.f -o ./noqual.o 

./npup.o: ../npup.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../npup.f -o ./npup.o 

./nrain.o: ../nrain.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../nrain.f -o ./nrain.o 

./nup.o: ../nup.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../nup.f -o ./nup.o 

./nuts.o: ../nuts.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../nuts.f -o ./nuts.o 

./openwth.o: ../openwth.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../openwth.f -o ./openwth.o 

./operatn.o: ../operatn.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../operatn.f -o ./operatn.o 

./orgn.o: ../orgn.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../orgn.f -o ./orgn.o 

./orgncswat.o: ../orgncswat.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../orgncswat.f -o ./orgncswat.o 

./origtile.o: ../origtile.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../origtile.f -o ./origtile.o 

./ovr_sed.o: ../ovr_sed.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow -ffixed-line-length-132 ../ovr_sed.f -o ./ovr_sed.o 

./percmacro.o: ../percmacro.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../percmacro.f -o ./percmacro.o 

./percmain.o: ../percmain.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow -ffixed-line-length-132 ../percmain.f -o ./percmain.o 

./percmicro.o: ../percmicro.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../percmicro.f -o ./percmicro.o 

./pestlch.o: ../pestlch.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pestlch.f -o ./pestlch.o 

./pestw.o: ../pestw.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pestw.f -o ./pestw.o 

./pesty.o: ../pesty.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pesty.f -o ./pesty.o 

./pgen.o: ../pgen.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pgen.f -o ./pgen.o 

./pgenhr.o: ../pgenhr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pgenhr.f -o ./pgenhr.o 

./pkq.o: ../pkq.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pkq.f -o ./pkq.o 

./plantmod.o: ../plantmod.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../plantmod.f -o ./plantmod.o 

./plantop.o: ../plantop.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../plantop.f -o ./plantop.o 

./pmeas.o: ../pmeas.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pmeas.f -o ./pmeas.o 

./pminrl.o: ../pminrl.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pminrl.f -o ./pminrl.o 

./pminrl2.o: ../pminrl2.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pminrl2.f -o ./pminrl2.o 

./pond.o: ../pond.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pond.f -o ./pond.o 

./pondhr.o: ../pondhr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pondhr.f -o ./pondhr.o 

./pothole.o: ../pothole.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../pothole.f -o ./pothole.o 

./potholehr.o: ../potholehr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../potholehr.f -o ./potholehr.o 

./print_hyd.o: ../print_hyd.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../print_hyd.f -o ./print_hyd.o 

./psed.o: ../psed.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../psed.f -o ./psed.o 

./qman.o: ../qman.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../qman.f -o ./qman.o 

./ran1.o: ../ran1.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../ran1.f -o ./ran1.o 

./rchaa.o: ../rchaa.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rchaa.f -o ./rchaa.o 

./rchday.o: ../rchday.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rchday.f -o ./rchday.o 

./rchinit.o: ../rchinit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rchinit.f -o ./rchinit.o 

./rchmon.o: ../rchmon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rchmon.f -o ./rchmon.o 

./rchuse.o: ../rchuse.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rchuse.f -o ./rchuse.o 

./rchyr.o: ../rchyr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rchyr.f -o ./rchyr.o 

./reachout.o: ../reachout.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../reachout.f -o ./reachout.o 

./readatmodep.o: ../readatmodep.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readatmodep.f -o ./readatmodep.o 

./readbsn.o: ../readbsn.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readbsn.f -o ./readbsn.o 

./readchm.o: ../readchm.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readchm.f -o ./readchm.o 

./readcnst.o: ../readcnst.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readcnst.f -o ./readcnst.o 

./readfcst.o: ../readfcst.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readfcst.f -o ./readfcst.o 

./readfert.o: ../readfert.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readfert.f -o ./readfert.o 

./readfig.o: ../readfig.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readfig.f -o ./readfig.o 

./readfile.o: ../readfile.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readfile.f -o ./readfile.o 

./readgw.o: ../readgw.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readgw.f -o ./readgw.o 

./readhru.o: ../readhru.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readhru.f -o ./readhru.o 

./readinpt.o: ../readinpt.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readinpt.f -o ./readinpt.o 

./readlup.o: ../readlup.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readlup.f -o ./readlup.o 

./readlwq.o: ../readlwq.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readlwq.f -o ./readlwq.o 

./readmgt.o: ../readmgt.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readmgt.f -o ./readmgt.o 

./readmon.o: ../readmon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readmon.f -o ./readmon.o 

./readops.o: ../readops.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readops.f -o ./readops.o 

./readpest.o: ../readpest.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readpest.f -o ./readpest.o 

./readplant.o: ../readplant.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readplant.f -o ./readplant.o 

./readpnd.o: ../readpnd.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readpnd.f -o ./readpnd.o 

./readres.o: ../readres.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readres.f -o ./readres.o 

./readrte.o: ../readrte.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readrte.f -o ./readrte.o 

./readru.o: ../readru.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readru.f -o ./readru.o 

./readsdr.o: ../readsdr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readsdr.f -o ./readsdr.o 

./readsepticbz.o: ../readsepticbz.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readsepticbz.f -o ./readsepticbz.o 

./readseptwq.o: ../readseptwq.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readseptwq.f -o ./readseptwq.o 

./readsno.o: ../readsno.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readsno.f -o ./readsno.o 

./readsol.o: ../readsol.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readsol.f -o ./readsol.o 

./readsub.o: ../readsub.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readsub.f -o ./readsub.o 

./readswq.o: ../readswq.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readswq.f -o ./readswq.o 

./readtill.o: ../readtill.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readtill.f -o ./readtill.o 

./readurban.o: ../readurban.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readurban.f -o ./readurban.o 

./readwgn.o: ../readwgn.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readwgn.f -o ./readwgn.o 

./readwus.o: ../readwus.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readwus.f -o ./readwus.o 

./readwwq.o: ../readwwq.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readwwq.f -o ./readwwq.o 

./readyr.o: ../readyr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../readyr.f -o ./readyr.o 

./reccnst.o: ../reccnst.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../reccnst.f -o ./reccnst.o 

./recday.o: ../recday.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../recday.f -o ./recday.o 

./rechour.o: ../rechour.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rechour.f -o ./rechour.o 

./recmon.o: ../recmon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../recmon.f -o ./recmon.o 

./recyear.o: ../recyear.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../recyear.f -o ./recyear.o 

./regres.o: ../regres.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../regres.f -o ./regres.o 

./res.o: ../res.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../res.f -o ./res.o 

./resbact.o: ../resbact.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../resbact.f -o ./resbact.o 

./resetlu.o: ../resetlu.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../resetlu.f -o ./resetlu.o 

./reshr.o: ../reshr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../reshr.f -o ./reshr.o 

./resinit.o: ../resinit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../resinit.f -o ./resinit.o 

./resnut.o: ../resnut.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../resnut.f -o ./resnut.o 

./rewind_init.o: ../rewind_init.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rewind_init.f -o ./rewind_init.o 

./rhgen.o: ../rhgen.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rhgen.f -o ./rhgen.o 

./rootfr.o: ../rootfr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rootfr.f -o ./rootfr.o 

./route.o: ../route.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../route.f -o ./route.o 

./routels.o: ../routels.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../routels.f -o ./routels.o 

./routeunit.o: ../routeunit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../routeunit.f -o ./routeunit.o 

./routres.o: ../routres.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../routres.f -o ./routres.o 

./rsedaa.o: ../rsedaa.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rsedaa.f -o ./rsedaa.o 

./rseday.o: ../rseday.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rseday.f -o ./rseday.o 

./rsedmon.o: ../rsedmon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rsedmon.f -o ./rsedmon.o 

./rsedyr.o: ../rsedyr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rsedyr.f -o ./rsedyr.o 

./rtbact.o: ../rtbact.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtbact.f -o ./rtbact.o 

./rtday.o: ../rtday.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtday.f -o ./rtday.o 

./rteinit.o: ../rteinit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rteinit.f -o ./rteinit.o 

./rthmusk.o: ../rthmusk.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rthmusk.f -o ./rthmusk.o 

./rthpest.o: ../rthpest.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rthpest.f -o ./rthpest.o 

./rthr.o: ../rthr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rthr.f -o ./rthr.o 

./rthsed.o: ../rthsed.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow -ffixed-line-length-132 ../rthsed.f -o ./rthsed.o 

./rtmusk.o: ../rtmusk.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtmusk.f -o ./rtmusk.o 

./rtout.o: ../rtout.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtout.f -o ./rtout.o 

./rtpest.o: ../rtpest.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtpest.f -o ./rtpest.o 

./rtsed.o: ../rtsed.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtsed.f -o ./rtsed.o 

./rtsed_bagnold.o: ../rtsed_bagnold.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtsed_bagnold.f -o ./rtsed_bagnold.o 

./rtsed_kodatie.o: ../rtsed_kodatie.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtsed_kodatie.f -o ./rtsed_kodatie.o 

./rtsed_molinas_wu.o: ../rtsed_Molinas_Wu.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtsed_Molinas_Wu.f -o ./rtsed_molinas_wu.o 

./rtsed_yangsand.o: ../rtsed_yangsand.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../rtsed_yangsand.f -o ./rtsed_yangsand.o 

./sat_excess.o: ../sat_excess.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../sat_excess.f -o ./sat_excess.o 

./save.o: ../save.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../save.f -o ./save.o 

./saveconc.o: ../saveconc.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../saveconc.f -o ./saveconc.o 

./schedule_ops.o: ../schedule_ops.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../schedule_ops.f -o ./schedule_ops.o 

./sched_mgt.o: ../sched_mgt.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../sched_mgt.f -o ./sched_mgt.o 

./simulate.o: ../simulate.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../simulate.f -o ./simulate.o 

./sim_initday.o: ../sim_initday.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../sim_initday.f -o ./sim_initday.o 

./sim_inityr.o: ../sim_inityr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../sim_inityr.f -o ./sim_inityr.o 

./slrgen.o: ../slrgen.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../slrgen.f -o ./slrgen.o 

./smeas.o: ../smeas.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../smeas.f -o ./smeas.o 

./snom.o: ../snom.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../snom.f -o ./snom.o 

./soil_chem.o: ../soil_chem.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../soil_chem.f -o ./soil_chem.o 

./soil_par.o: ../soil_par.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../soil_par.f -o ./soil_par.o 

./soil_phys.o: ../soil_phys.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../soil_phys.f -o ./soil_phys.o 

./soil_write.o: ../soil_write.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../soil_write.f -o ./soil_write.o 

./solp.o: ../solp.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../solp.f -o ./solp.o 

./solt.o: ../solt.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../solt.f -o ./solt.o 

./std1.o: ../std1.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../std1.f -o ./std1.o 

./std2.o: ../std2.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../std2.f -o ./std2.o 

./std3.o: ../std3.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../std3.f -o ./std3.o 

./stdaa.o: ../stdaa.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../stdaa.f -o ./stdaa.o 

./storeinitial.o: ../storeinitial.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../storeinitial.f -o ./storeinitial.o 

./structure.o: ../structure.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../structure.f -o ./structure.o 

./subaa.o: ../subaa.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../subaa.f -o ./subaa.o 

./subbasin.o: ../subbasin.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../subbasin.f -o ./subbasin.o 

./subday.o: ../subday.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../subday.f -o ./subday.o 

./submon.o: ../submon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../submon.f -o ./submon.o 

./substor.o: ../substor.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../substor.f -o ./substor.o 

./subwq.o: ../subwq.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../subwq.f -o ./subwq.o 

./subyr.o: ../subyr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../subyr.f -o ./subyr.o 

./sub_subbasin.o: ../sub_subbasin.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../sub_subbasin.f -o ./sub_subbasin.o 

./sumhyd.o: ../sumhyd.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../sumhyd.f -o ./sumhyd.o 

./sumv.o: ../sumv.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../sumv.f -o ./sumv.o 

./surface.o: ../surface.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../surface.f -o ./surface.o 

./surfstor.o: ../surfstor.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../surfstor.f -o ./surfstor.o 

./surfst_h2o.o: ../surfst_h2o.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../surfst_h2o.f -o ./surfst_h2o.o 

./surq_daycn.o: ../surq_daycn.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../surq_daycn.f -o ./surq_daycn.o 

./surq_greenampt.o: ../surq_greenampt.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../surq_greenampt.f -o ./surq_greenampt.o 

./swbl.o: ../swbl.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../swbl.f -o ./swbl.o 

./sweep.o: ../sweep.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../sweep.f -o ./sweep.o 

./swu.o: ../swu.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../swu.f -o ./swu.o 

./tair.o: ../tair.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../tair.f -o ./tair.o 

./tgen.o: ../tgen.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../tgen.f -o ./tgen.o 

./theta.o: ../theta.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../theta.f -o ./theta.o 

./tillfactor.o: ../tillfactor.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../tillfactor.f -o ./tillfactor.o 

./tillmix.o: ../tillmix.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../tillmix.f -o ./tillmix.o 

./tmeas.o: ../tmeas.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../tmeas.f -o ./tmeas.o 

./tran.o: ../tran.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../tran.f -o ./tran.o 

./transfer.o: ../transfer.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../transfer.f -o ./transfer.o 

./tstr.o: ../tstr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../tstr.f -o ./tstr.o 

./ttcoef.o: ../ttcoef.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../ttcoef.f -o ./ttcoef.o 

./ttcoef_wway.o: ../ttcoef_wway.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../ttcoef_wway.f -o ./ttcoef_wway.o 

./urban.o: ../urban.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../urban.f -o ./urban.o 

./urbanhr.o: ../urbanhr.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../urbanhr.f -o ./urbanhr.o 

./urb_bmp.o: ../urb_bmp.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../urb_bmp.f -o ./urb_bmp.o 

./varinit.o: ../varinit.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../varinit.f -o ./varinit.o 

./vbl.o: ../vbl.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../vbl.f -o ./vbl.o 

./virtual.o: ../virtual.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../virtual.f -o ./virtual.o 

./volq.o: ../volq.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../volq.f -o ./volq.o 

./washp.o: ../washp.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../washp.f -o ./washp.o 

./watbal.o: ../watbal.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../watbal.f -o ./watbal.o 

./water_hru.o: ../water_hru.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../water_hru.f -o ./water_hru.o 

./watqual.o: ../watqual.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../watqual.f -o ./watqual.o 

./watqual2.o: ../watqual2.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../watqual2.f -o ./watqual2.o 

./wattable.o: ../wattable.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../wattable.f -o ./wattable.o 

./watuse.o: ../watuse.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../watuse.f -o ./watuse.o 

./weatgn.o: ../weatgn.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../weatgn.f -o ./weatgn.o 

./wetlan.o: ../wetlan.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../wetlan.f -o ./wetlan.o 

./wmeas.o: ../wmeas.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../wmeas.f -o ./wmeas.o 

./wndgen.o: ../wndgen.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../wndgen.f -o ./wndgen.o 

./writea.o: ../writea.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../writea.f -o ./writea.o 

./writeaa.o: ../writeaa.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../writeaa.f -o ./writeaa.o 

./writed.o: ../writed.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../writed.f -o ./writed.o 

./writem.o: ../writem.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../writem.f -o ./writem.o 

./xmon.o: ../xmon.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../xmon.f -o ./xmon.o 

./ysed.o: ../ysed.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../ysed.f -o ./ysed.o 

./zero0.o: ../zero0.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../zero0.f -o ./zero0.o 

./zero1.o: ../zero1.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../zero1.f -o ./zero1.o 

./zero2.o: ../zero2.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../zero2.f -o ./zero2.o 

./zeroini.o: ../zeroini.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../zeroini.f -o ./zeroini.o 

./zero_urbn.o: ../zero_urbn.f
	${FC} -c -O3 -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow ../zero_urbn.f -o ./zero_urbn.o 

clean:
	rm -f ${NAME} $
	rm -f *.o
	rm -f *.mod
	rm -f *~
