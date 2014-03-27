      subroutine headersqlite

!!    ~ ~ ~ PURPOSE ~ ~ ~                                               
!!    This subroutine defines header titles for the different output files

!!    ~ ~ ~ OUTGOING VARIABLES ~ ~ ~                                    
!!    name        |units         |definition
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ 
!!    hedb(:)     |NA            |column titles in subbasin output file
!!    hedr(:)     |NA            |column titles in reach output file
!!    hedrsv(:)   |NA            |column titles in reservoir output file
!!    heds(:)     |NA            |column titles in HRU output file
!!    hedwtr(:)   |NA            |column titles in HRU impoundment output 
!!                               |file
!!    icolb(:)    |none          |space number for beginning of column in
!!                               |subbasin output file
!!    icolr(:)    |none          |space number for beginning of column in
!!                               |reach output file
!!    icolrsv(:)  |none          |space number for beginning of column in
!!                               |reservoir output file
!!    icols(:)    |none          |space number for beginning of column in
!!                               |HRU output file
!!    ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ 

!!    ~ ~ ~ ~ ~ ~ END SPECIFICATIONS ~ ~ ~ ~ ~ ~

!!    ~ ~ ~ SQLITE ~ ~ ~
!!    1. To create column in SQLite, replace space and - with underscore _
!!    2. delete (), replace ( with underscore _
!!    3. delete arrays for VB interface output. They are not used any more
!!    4. change m^3 to m3
!!    ~ ~ ~ END SQLITE ~ ~ ~

      use parm

!!    column headers for HRU output file
      heds = (/"  PRECIPmm"," SNOFALLmm"," SNOMELTmm","     IRRmm",     
     &         "     PETmm","      ETmm"," SW_INITmm","  SW_ENDmm",     
     &         "    PERCmm"," GW_RCHGmm"," DA_RCHGmm","   REVAPmm",     
     &         "  SA_IRRmm","  DA_IRRmm","   SA_STmm","   DA_STmm",     
     &         "SURQ_GENmm","SURQ_CNTmm","   TLOSSmm"," LATQGENmm",     
     &         "    GW_Qmm","    WYLDmm","   DAILYCN"," TMP_AVdgC",     
     &         " TMP_MXdgC"," TMP_MNdgC","SOL_TMPdgC","SOLARMJ_m2",
     &         "  SYLDt_ha","  USLEt_ha","N_APPkg_ha","P_APPkg_ha",
     &         "NAUTOkg_ha","PAUTOkg_ha"," NGRZkg_ha"," PGRZkg_ha",
     &         "NCFRTkg_ha","PCFRTkg_ha","NRAINkg_ha"," NFIXkg_ha",
     &         " F_MNkg_ha"," A_MNkg_ha"," A_SNkg_ha"," F_MPkg_ha",
     &         "AO_LPkg_ha"," L_APkg_ha"," A_SPkg_ha"," DNITkg_ha",
     &         "  NUPkg_ha","  PUPkg_ha"," ORGNkg_ha"," ORGPkg_ha",
     &         " SEDPkg_ha","NSURQkg_ha","NLATQkg_ha"," NO3Lkg_ha",
     &         "NO3GWkg_ha"," SOLPkg_ha"," P_GWkg_ha","    W_STRS",
     &         "  TMP_STRS","    N_STRS","    P_STRS","  BIOMt_ha",
     &         "       LAI","   YLDt_ha","  BACTPct ","  BACTLPct",
     &         " WTAB_CLIm"," WTAB_SOLm","     SNOmm"," CMUPkg_ha",
     &         "CMTOTkg_ha","   QTILEmm"," TNO3kg_ha"," LNO3kg_ha",
     &         "  GW_Q_Dmm"," LATQCNTmm"/)

!!    column headers for subbasin output file
      hedb = (/"  PRECIPmm"," SNOMELTmm","     PETmm","      ETmm",     
     &         "      SWmm","    PERCmm","    SURQmm","    GW_Qmm",     
     &         "    WYLDmm","  SYLDt_ha"," ORGNkg_ha"," ORGPkg_ha",
     &         "NSURQkg_ha"," SOLPkg_ha"," SEDPkg_ha","  LAT_Q_mm",
     &         "LATNO3kg_h","GWNO3kg_ha","CHOLAmic_L","CBODU_mg_L",
     &         " DOXQ_mg_L"," TNO3kg_ha"/)

!!  added headers TOTAL N/TOTALP/NO3 Concentration TO HEADING FOR OUTPUT.RCH GSM 10/26/2011
!!    column headers for reach output file
      hedr = (/"  FLOW_INcms"," FLOW_OUTcms","     EVAPcms",            
     &         "    TLOSScms","  SED_INtons"," SED_OUTtons",            
     &         "SEDCONCmg_kg","   ORGN_INkg","  ORGN_OUTkg",
     &         "   ORGP_INkg","  ORGP_OUTkg","    NO3_INkg",            
     &         "   NO3_OUTkg","    NH4_INkg","   NH4_OUTkg",            
     &         "    NO2_INkg","   NO2_OUTkg","   MINP_INkg",            
     &         "  MINP_OUTkg","   CHLA_INkg","  CHLA_OUTkg",            
     &         "   CBOD_INkg","  CBOD_OUTkg","  DISOX_INkg",            
     &         " DISOX_OUTkg"," SOLPST_INmg","SOLPST_OUTmg",            
     &         " SORPST_INmg","SORPST_OUTmg","  REACTPSTmg",          
     &         "    VOLPSTmg","  SETTLPSTmg","RESUSP_PSTmg",            
     &         "DIFFUSEPSTmg","REACBEDPSTmg","   BURYPSTmg",            
     &         "   BED_PSTmg"," BACTP_OUTct","BACTLP_OUTct",            
     &         "  CMETAL_1kg","  CMETAL_2kg","  CMETAL_3kg",
     &         "     TOT_Nkg","     TOT_Pkg"," NO3ConcMg_l",
     &         "    WTMPdegc"/)

!!    column headers for reservoir output file
      hedrsv = (/"    VOLUMEm3","  FLOW_INcms"," FLOW_OUTcms",          
     &           "    PRECIPm3","      EVAPm3","   SEEPAGEm3",          
     &           "  SED_INtons"," SED_OUTtons"," SED_CONCppm",          
     &           "   ORGN_INkg","  ORGN_OUTkg"," RES_ORGNppm",          
     &           "   ORGP_INkg","  ORGP_OUTkg"," RES_ORGPppm",          
     &           "    NO3_INkg","   NO3_OUTkg","  RES_NO3ppm",          
     &           "    NO2_INkg","   NO2_OUTkg","  RES_NO2ppm",          
     &           "    NH3_INkg","   NH3_OUTkg","  RES_NH3ppm",          
     &           "   MINP_INkg","  MINP_OUTkg"," RES_MINPppm",          
     &           "   CHLA_INkg","  CHLA_OUTkg","SECCHIDEPTHm",          
     &           "   PEST_INmg","  REACTPSTmg","    VOLPSTmg",          
     &           "  SETTLPSTmg","RESUSP_PSTmg","DIFFUSEPSTmg",          
     &           "REACBEDPSTmg","   BURYPSTmg","  PEST_OUTmg",          
     &           "PSTCNCWmg_m3","PSTCNCBmg_m3"/)

!!    column headers for HRU impoundment output file
      hedwtr = (/"  PNDPCPmm","  PND_INmm","PSED_It_ha","  PNDEVPmm",
     &           "  PNDSEPmm"," PND_OUTmm","PSED_Ot_ha","  PNDVOLm3",
     &           "PNDORGNppm"," PNDNO3ppm","PNDORGPppm","PNDMINPppm",   
     &           "PNDCHLAppm","  PNDSECIm","  WETPCPmm","  WET_INmm",   
     &           "WSED_It_ha","  WETEVPmm","  WETSEPmm"," WET_OUTmm",
     &           "WSED_Ot_ha","  WETVOLm3","WETORGNppm"," WETNO3ppm",
     &           "WETORGPppm","WETMINPppm","WETCHLAppm","  WETSECIm",   
     &           "  POTPCPmm","  POT_INmm","OSED_It_ha","  POTEVPmm",
     &           "  POTSEPmm"," POT_OUTmm","OSED_Ot_ha","  POTVOLm3",
     &           "  POT_SAha","HRU_SURQmm","PLANT_ETmm"," SOIL_ETmm"/)

      return
      end                                           
