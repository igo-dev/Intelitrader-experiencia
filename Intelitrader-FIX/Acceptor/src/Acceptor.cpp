#include "Acceptor.h"

int main( int argc, char** argv )
{

  std::string file = "acceptor.cfg";

  try
  {
    FIX::SessionSettings settings( file );

    Application application;
    FIX::FileStoreFactory storeFactory( settings );
    FIX::FileLogFactory logFactory( settings );
    FIX::SocketAcceptor acceptor( application, storeFactory, settings, logFactory );

    acceptor.start();
    while (true) {};
    acceptor.stop();
    return 0;
  }
  catch ( std::exception & e )
  {
    std::cout << e.what() << std::endl;
    return 1;
  }
}
