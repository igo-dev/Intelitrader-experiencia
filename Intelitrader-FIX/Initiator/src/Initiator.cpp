#include "Initiator.h"

int main(int argc, char** argv)
{
    try
    {
        FIX::SessionSettings settings("initiator.cfg");
        Application application;
        FIX::FileStoreFactory storeFactory(settings);
        FIX::ScreenLogFactory logFactory(settings);
        FIX::SocketInitiator initiator(application, storeFactory, settings);

        initiator.start();
        application.run();
        initiator.stop();

        return 0;
    }
    catch (std::exception& e)
    {
        std::cout << e.what();
        return 1;
    }
}
