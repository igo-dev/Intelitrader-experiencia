cmake_minimum_required(VERSION 3.20)
set(CMAKE_CXX_STANDARD 11)

project(Inteli-Acceptor)

add_executable(${PROJECT_NAME} src/Order.h src/Order.cpp src/Acceptor.h src/Acceptor.cpp src/Application.h src/Application.cpp)

include_directories(${CMAKE_SOURCE_DIR}/libs/quickfix/include)

target_link_libraries(${PROJECT_NAME} ${CMAKE_SOURCE_DIR}/libs/quickfix/lib/quickfix.lib wsock32 ws2_32)