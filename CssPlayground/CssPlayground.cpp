
#include <windows.h>
#include <thread>
#include <sysinfoapi.h>
#include <ios>
#include <iostream>
#include <fstream>
#include <string>
#include "../CppTestFramework/TestContext.h"
#include "../CppTestFramework/Assert.h"


#define DIV 1048576
#define WIDTH 7

void load()
{
    while (true)
    {

    }
}

DWORDLONG mem_usage() {
    MEMORYSTATUSEX statex;

    statex.dwLength = sizeof(statex);

    GlobalMemoryStatusEx(&statex);

    // std::cout << (TEXT("There is  %*ld percent of memory in use.\n"), WIDTH, statex.dwMemoryLoad) << std::endl;
    // std::cout << (TEXT("There are %*I64d total Mbytes of physical memory.\n"), WIDTH, statex.ullTotalPhys / DIV) << std::endl;
    // std::cout << "There are " << statex.ullAvailPhys << " free bytes of physical memory." << std::endl;
    //std::cout << (TEXT("There are %*I64d total Mbytes of paging file.\n"), WIDTH, statex.ullTotalPageFile / DIV) << std::endl;
    //std::cout << "There are " << statex.ullAvailPageFile << " free Mbytes of paging file." << std::endl;
    //std::cout << (TEXT("There are %*I64d total Mbytes of virtual memory.\n"), WIDTH, statex.ullTotalVirtual / DIV) << std::endl;
    //std::cout << "There are " << statex.ullAvailPageFile << " free Mbytes of virtual memory." << std::endl;
    //std::cout << (TEXT("There are %*I64d free Mbytes of extended memory.\n"), WIDTH, statex.ullAvailExtendedVirtual / DIV) << std::endl;
    return statex.ullAvailPhys;
}

void testMetdhod()
{
    MEMORYSTATUSEX statex1;
    MEMORYSTATUSEX statex2;

    statex1.dwLength = sizeof(statex1);
    GlobalMemoryStatusEx(&statex1);

    DWORDLONG start = mem_usage();

    int mem = 0;
    double* a = new double();

    GlobalMemoryStatusEx(&statex2);

    DWORDLONG end = mem_usage();

    //std::cout << "Virtual" << "\t\t" << statex1.ullAvailVirtual << "\t\t" << statex2.ullAvailVirtual << std::endl;
    //std::cout << "Phys" << "\t\t" << statex1.ullAvailPhys << "\t\t" << statex2.ullAvailPhys << std::endl;
    //std::cout << "Page File" << "\t\t" << statex1.ullAvailPageFile << "\t\t" << statex2.ullAvailPageFile << std::endl;

    //std::cout << "Virtual" << "\t\t" << statex2.ullAvailVirtual - statex1.ullAvailVirtual << std::endl;
    //std::cout << "Phys" << "\t\t" << statex2.ullAvailPhys - statex1.ullAvailPhys << std::endl;
    //std::cout << "Page File" << "\t\t" << statex2.ullAvailPageFile - statex1.ullAvailPageFile << std::endl;

    std::cout << start - end << std::endl;
}

std::chrono::system_clock::time_point now()
{
    return std::chrono::system_clock::now();
}

struct DoneToken
{
    bool Done;
};

class Solution
{
public:
    std::string mid(std::vector<std::string> arr, std::string returnThis)
    {
        return "";
    }
};

class TestCase
{
public:
    TestCase(int Id, std::string Expected, std::vector<std::string> arr, std::string returnThis)
    {
        this->Id = Id;
        this->Expected = Expected;
        this->arr = arr;

        this->returnThis = returnThis;
    }
    int Id;
    std::string Expected;
    std::vector<std::string> arr;

    std::string returnThis;
};
class TestCaseFactory
{
public:

    static void Init(const std::list<TestCase>& testCases)
    {
        if (!Instance)
        {
            delete Instance;
        }
        Instance = new TestCaseFactory(testCases);
    }

    static std::string ExecuteNext()
    {
        Solution solution;
        TestCase testCase = *Instance->Current;

        std::string result = solution.mid(testCase.arr, testCase.returnThis);

        Instance->Current++;

        return result;
    }

    static int Id()
    {
        return (*Instance->Current).Id;
    }

    static std::string Expected()
    {
        return (*Instance->Current).Expected;
    }

private:
    std::list<TestCase> TestCases;
    std::list<TestCase>::iterator Current;
    static TestCaseFactory* Instance;

    TestCaseFactory(const std::list<TestCase>& testCases)
    {
        TestCases = testCases;
        Current = TestCases.begin();
    }
    ~TestCaseFactory()
    {
        if (!Instance)
        {
            delete Instance;
        }
    }
};
TestCaseFactory* TestCaseFactory::Instance = nullptr;


int main()
{
    // testMetdhod();
    std::thread timeout;
    std::chrono::duration<double> elapsed_time;
    DoneToken* taskToken = new DoneToken();
    try
    {
        int timelimitMiliseconds = 60000;
        double timelimitSeconds = timelimitMiliseconds / 1000.0;
        std::chrono::system_clock::time_point start = now();
        timeout = std::thread([taskToken]()
        {
            try
            {
                std::list<TestCase> testCases
                {
                    TestCase(0, "b", {"a","b","c"},"b"),
            TestCase(1, "c", {"c","b","c"},"c"),
            TestCase(2, "q", {"q","a","q"},"q")
                };
                TestCaseFactory::Init(testCases);
                TestContext context;
                context.run(TestCaseFactory::ExecuteNext, TestCaseFactory::Expected, TestCaseFactory::Id, testCases.size());
            }
            catch (const std::exception &ex)
            {
                std::cout << ex.what() << std::endl;
            }
            taskToken->Done = true;
        });

        for (auto start = now(); !taskToken->Done; elapsed_time = now() - start)
        {
            if (elapsed_time.count() >= timelimitSeconds)
            {
                throw std::runtime_error("Time limit exceeded.");
            }
        }
        
        mem_usage();
        // testMetdhod();
        std::string testString("test1");
        Assert::AreEqual(testString, testString);
        mem_usage();

    }
    catch (std::exception ex)
    {
        std::cout << ex.what() << std::endl;
    }

    timeout.detach();
    delete taskToken;

    std::cout << "All done." << std::endl;
    std::cout << "Time elapsed " << elapsed_time.count() << std::endl;
}