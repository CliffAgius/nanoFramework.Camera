#
# Copyright (c) .NET Foundation and Contributors
# See LICENSE file in the project root for full license information.
#

########################################################################################
# make sure that a valid path is set bellow                                            #
# this is an Interop module so this file should be placed in the CMakes module folder  #
# usually CMake\Modules                                                                #
########################################################################################

# native code directory
set(BASE_PATH_FOR_THIS_MODULE ${PROJECT_SOURCE_DIR}/InteropAssemblies/nanoFramework.Camera)


# set include directories
list(APPEND nanoFramework.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Core)
list(APPEND nanoFramework.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Include)
list(APPEND nanoFramework.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/HAL/Include)
list(APPEND nanoFramework.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/PAL/Include)
list(APPEND nanoFramework.Camera_INCLUDE_DIRS ${BASE_PATH_FOR_THIS_MODULE})


# source files
set(nanoFramework.Camera_SRCS

    nanoFramework_Camera.cpp


    nanoFramework_Camera_nanoFramework_Camera_Camera_mshl.cpp
    nanoFramework_Camera_nanoFramework_Camera_Camera.cpp

)

foreach(SRC_FILE ${nanoFramework.Camera_SRCS})

    set(nanoFramework.Camera_SRC_FILE SRC_FILE-NOTFOUND)

    find_file(nanoFramework.Camera_SRC_FILE ${SRC_FILE}
        PATHS
	        ${BASE_PATH_FOR_THIS_MODULE}
	        ${TARGET_BASE_LOCATION}
            ${PROJECT_SOURCE_DIR}/src/nanoFramework.Camera

	    CMAKE_FIND_ROOT_PATH_BOTH
    )

    if (BUILD_VERBOSE)
        message("${SRC_FILE} >> ${nanoFramework.Camera_SRC_FILE}")
    endif()

    list(APPEND nanoFramework.Camera_SOURCES ${nanoFramework.Camera_SRC_FILE})

endforeach()

include(FindPackageHandleStandardArgs)

FIND_PACKAGE_HANDLE_STANDARD_ARGS(nanoFramework.Camera DEFAULT_MSG nanoFramework.Camera_INCLUDE_DIRS nanoFramework.Camera_SOURCES)
