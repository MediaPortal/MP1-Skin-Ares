#region Copyright (C) 2005-2017 Team MediaPortal
/*
// Copyright (C) 2005-2011 Team MediaPortal
// http://www.team-mediaportal.com
// 
// MediaPortal is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MediaPortal is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MediaPortal. If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

#**********************************************************************************************************#
#
#   For building the installer on your own you need:
#       1. Latest NSIS version from http://nsis.sourceforge.net/Download
#
#**********************************************************************************************************#

#---------------------------------------------------------------------------
# SPECIAL BUILDS
#---------------------------------------------------------------------------
##### GIT_BUILD
# This build will be created by git bot only.
# Creating such a build, will only include the changed and new files since latest stable release to the installer.

##### HEISE_BUILD
# Uncomment the following line to create a setup for "Heise Verlag" / ct' magazine  (without MPC-HC/Gabest Filters)
#!define HEISE_BUILD
# parameter for command line execution: /DHEISE_BUILD


#---------------------------------------------------------------------------
# DEVELOPMENT ENVIRONMENT
#---------------------------------------------------------------------------

#---------------------------------------------------------------------------
# DEVELOPMENT ENVIRONMENT
#---------------------------------------------------------------------------

##---------------------------------------------------------------------------
## pre build commands
##---------------------------------------------------------------------------

##---------------------------------------------------------------------------
## DEFINES
##---------------------------------------------------------------------------

#---------------------------------------------------------------------------
# VARIABLES
#---------------------------------------------------------------------------


#---------------------------------------------------------------------------
# INCLUDE FILES
#---------------------------------------------------------------------------


#---------------------------------------------------------------------------
# INSTALLER INTERFACE settings
#---------------------------------------------------------------------------


#---------------------------------------------------------------------------
# INSTALLER INTERFACE
#---------------------------------------------------------------------------


#---------------------------------------------------------------------------
# INSTALLER LANGUAGES
#---------------------------------------------------------------------------



#---------------------------------------------------------------------------
# INSTALLER ATTRIBUTES
#---------------------------------------------------------------------------
!define name_output "Ares ${ReleaseVersion}"
!define Ext_filename "Ares skin.xmp2"   
!define ReleaseVersion "${VER_MAJOR}.${VER_MINOR}.${VER_REVISION}.${VER_BUILD}"

#test command
#!system '"${GIT_ROOT}\mediaportal\MPE\MpeMaker\bin\Release\MpeMaker.exe"  "${AresPath}\${Ext_filename}" /V=0.0.0.3 /B'

!system '"${GIT_ROOT}\mediaportal\MPE\MpeMaker\bin\${BUILD_TYPE}\MpeMaker.exe"  "${AresPath}\${Ext_filename}" /V=${ReleaseVersion} /B' = 0 

#move the extension installer to Ares skin root folder. 
!system 'move "${git_InstallScripts}\${name_output}.mpe1" "${AresPath}\Ares.mpe1"'



#---------------------------------------------------------------------------
# USEFUL MACROS
#---------------------------------------------------------------------------



#---------------------------------------------------------------------------
# SECTIONS and REMOVEMACROS
#---------------------------------------------------------------------------


  #---------------------------------------------------------------------------
  # FILTER REGISTRATION
  #               for more information see:           http://nsis.sourceforge.net/Docs/AppendixB.html
  #---------------------------------------------------------------------------


#---------------------------------------------------------------------------
# This Section is executed after the Main section has finished and writes Uninstall information into the registry

#---------------------------------------------------------------------------
# This section is called on uninstall and removes all components


#---------------------------------------------------------------------------
# SOME MACROS AND FUNCTIONS
#---------------------------------------------------------------------------



#---------------------------------------------------------------------------
# SECTION DESCRIPTIONS
#---------------------------------------------------------------------------
