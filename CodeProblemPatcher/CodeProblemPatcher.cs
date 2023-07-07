using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data.Enums;
using Gateway.Api.Interfaces;
using Gateway.Contact.Models;
using Mongo.Data.Services;
using Shared.Core.Extensions;
using TestCases.Providers;

namespace CodeProblemPatcher
{
    public class CodeProblemLoader
    {
        private readonly ICodeProblemProvider _codeProblemService;
        private readonly ITestCaseService _testCaseService;
        private readonly ITestCasesProvider _testCasesProvider;

        public CodeProblemLoader(ICodeProblemProvider codeProblemService, ITestCaseService testCaseService, ITestCasesProvider testCasesProvider)
        {
            _codeProblemService = codeProblemService.ThrowIfNull();
            _testCaseService = testCaseService.ThrowIfNull();
            _testCasesProvider = testCasesProvider.ThrowIfNull();
        }

        public async Task Load()
        {
            var codeProblem = new CreateCodeProblemRequest
            {
                ComplexityTypeId = CodeProblemComplexity.Easy,
                Constraints = new List<string>()
                {
                    "The input list nums will contain at least two integers.",
                    "The integers in nums will be in the range [-1000, 1000].",
                },
                Description = "Write a function that takes in a list of integers and returns the two numbers that appear the most frequently. If there are multiple pairs with the same maximum frequency, return the pair with the smallest sum.\r\n\r\nFor example, given the input list [1, 2, 3, 2, 1, 3, 4, 5, 4, 4], the function should return (4, 4) because 4 and 2 both appear three times, but (4, 4) has the smallest sum.",
                Name = "Find Most Frequent Pair",
                Examples = new List<Example>()
                {
                    new Example("[1, 2, 3, 2, 1, 3, 4, 5, 4, 4]", "(2, 2)"),
                },
                ParameterNames = new List<string>()
                {
                    "nums",
                },
                ParameterTypes = new()
                {
                    InternalType._Array | InternalType._Int32,
                },
                ReturnType = InternalType._Array | InternalType._Int32,
                TestCaseSetUUID = Guid.Parse("fa6dc121-02c6-4496-ab0f-5fb8ea83ce2d"),
                Tags = new List<string>()
                {
                    "array",
                    "search",
                }
            };
            await LoadSingle(codeProblem);

            codeProblem = new()
            {
                Description = "Write a function that takes in a string as input and determines whether it is a palindrome or not. A palindrome is a word, phrase, number, or other sequence of characters that reads the same forward and backward, ignoring spaces, punctuation, and capitalization.",
                Name = "palindrome",
                Examples = new()
                {
                    new("racecar", "true"),
                    new("A man, a plan, a canal: Panama", "true"),
                    new("hello", "false"),
                },
                Constraints = new()
                {
                    "Input string length <= 10^5",
                },
                ComplexityTypeId = CodeProblemComplexity.Easy,
                ParameterNames = new()
                {
                    "str",
                },
                ParameterTypes = new()
                {
                    InternalType._String,
                },
                ReturnType = InternalType._Bool,
                Tags = new()
                {
                    "string",
                    "palindrome",
                },
                TestCaseSetUUID = Guid.Parse("2957c7bc-a211-432b-95ed-d556d950691c"),
            };
            await LoadSingle(codeProblem);

            codeProblem = new()
            {
                Name = "Product of Array Except Self",
                Description = "You have to implement a function productExceptSelf(nums: List[int]) -> List[int] where nums is an array of n integers where n > 1. The function should return an array output such that output[i] is equal to the product of all the elements of nums except nums[i].",
                ReturnType = InternalType._Array | InternalType._Int32,
                ParameterTypes = new()
                {
                    InternalType._Array | InternalType._Int32,
                },
                ParameterNames = new()
                {
                    "nums",
                },
                Constraints = new()
                {
                    "Solve this problem without division and in O(n).",
                    "Solve this problem with constant space complexity. (The output array does not count as extra space for the purpose of space complexity analysis.)",
                },
                Tags = new()
                {
                    "array",
                    "product",
                },
                Examples = new()
                {
                    new("[1,2,3,4]", "[24,12,8,6]"),
                },
                Explanation = "The key to solving this problem without using division and in O(n) time is understanding that the product of all numbers to the left and to the right of a number is the product of all numbers except itself.\r\n\r\nLet's look at an example, given nums = [1, 2, 3, 4].\r\n\r\nWe will first calculate the product of all numbers to the left of each number.\r\n\r\nFor nums[0] (1), there are no numbers to its left, so the product is 1.\r\nFor nums[1] (2), the product to its left is 1 (only nums[0]), so the product is 1.\r\nFor nums[2] (3), the product to its left is 1 * 2 = 2.\r\nFor nums[3] (4), the product to its left is 1 * 2 * 3 = 6.\r\nThen we calculate the product of all numbers to the right of each number. We do this from the end of the array, right to left.\r\n\r\nFor nums[3] (4), there are no numbers to its right, so the product is 1.\r\nFor nums[2] (3), the product to its right is 4 (only nums[3]), so the product is 4.\r\nFor nums[1] (2), the product to its right is 4 * 3 = 12.\r\nFor nums[0] (1), the product to its right is 4 * 3 * 2 = 24.\r\nThe final output is the product of the left and right products for each number.\r\n\r\nFor nums[0] (1), the product is 1 * 24 = 24.\r\nFor nums[1] (2), the product is 1 * 12 = 12.\r\nFor nums[2] (3), the product is 2 * 4 = 8.\r\nFor nums[3] (4), the product is 6 * 1 = 6.\r\nThis gives us the final output [24, 12, 8, 6].\r\n\r\nThe space complexity of this solution is O(1) because we only use a constant amount of space to store the output array and two variables to store the left and right products. The time complexity is O(n) because we do one pass through the array for the left products and another pass for the right products.",
                TestCaseSetUUID = Guid.Parse("7b4cb5dc-e41a-4392-9bbb-fc46646ca553"),
                ComplexityTypeId = CodeProblemComplexity.Easy,
            };
            await LoadSingle(codeProblem);

            codeProblem = new()
            {
                Description = "Write a function that takes two lists of integers as inputs and return a new list that combines the elements of the two lists in an alternating manner.\r\nIf one list is longer than the other, append the remaining elements of the longer list to the end of the new list. If either of the lists is empty, return the other list as is. If both lists are empty, return an empty list.",
                Name = "Merge Alternate",
                ParameterNames = new()
                {
                    "list1",
                    "list2",
                },
                ParameterTypes = new()
                {
                    InternalType._Array | InternalType._Int32,
                    InternalType._Array | InternalType._Int32,
                },
                ReturnType = InternalType._Array | InternalType._Int32,
                Examples = new()
                {
                    new("[1, 2, 3], [4, 5, 6]", "[1, 4, 2, 5, 3, 6]"),
                    new("[1, 2, 3], [4, 5]", "[1, 4, 2, 5, 3]"),
                },
                ComplexityTypeId = CodeProblemComplexity.Medium,
                Tags = new()
                {
                    "array",
                    "merge",
                },
                TestCaseSetUUID = Guid.Parse("f47001c2-7614-4851-9c29-4d4c6fa3a1bd"),
                Explanation = "We want to create a new list that contains elements from two input lists in an alternating manner. The pseudocode for this solution could look like this:\r\n\r\nInitialize an empty list that will store our result.\r\n\r\nDetermine the length of the shortest input list.\r\n\r\nLoop over the range of the length of the shortest list. On each iteration, append the corresponding elements from both lists to our result list.\r\n\r\nAfter the loop, if one list was longer than the other, there will be remaining elements in that list. We need to append these elements to our result list.\r\n\r\nReturn the result list.\r\n\r\nThe key here is to handle the case where the lists are of unequal length. By iterating over the range of the length of the shortest list, we ensure that we don't run into an 'index out of range' error. After this loop, we simply append the remaining elements of the longer list (if any) to the result list.\r\n\r\nThe time complexity for this solution is O(n), where n is the length of the longest list. This is because we loop through each list at most once. The space complexity is also O(n) for the same reason - we create a new list that contains all the elements of the input lists."
            };
            await LoadSingle(codeProblem);
            //var dummy = GetDummyProblems();

            codeProblem = new()
            {
                Name = "Three Sum",
                Description = "Write a function that determines whether there exist three elements in a given list that sum up to a target value.\nIf no such elements exist, the function should return emtpy array.\n\nThe challenge here lies in avoiding the brute force solution, which has a time complexity of O(n^3). You should aim for a solution with a time complexity of O(n^2).",
                ParameterNames = new()
                {
                    "nums",
                    "target",
                },
                ParameterTypes = new()
                {
                    InternalType._Array | InternalType._Int32,
                    InternalType._Int32,
                },
                ReturnType = InternalType._Array | InternalType._Int32,
                Examples = new()
                {
                    new("[1, 2, 3, 4, 5], 10", "[1, 4, 5]"),
                    new("[10, 20, 30, 40, 50], 100", "[]"),
                },
                ComplexityTypeId = CodeProblemComplexity.Hard,
                TestCaseSetUUID = Guid.Parse("1980fa58-931c-457f-b22f-73aa9b81610f"),
                Explanation = "The naive solution to this problem is a brute-force method where you would use three nested loops to try every possible combination of three numbers. However, this approach has a time complexity of O(n^3), which is not efficient for large input lists.\r\n\r\nA more efficient approach would be to use a technique similar to the Two Pointers method. The main idea is to iterate over the list, fix one number, and then find the other two numbers such that their sum equals the target. Here's a step-by-step approach:\r\n\r\nSort the input list in ascending order.\r\n\r\nInitialize a variable n to hold the length of the list.\r\n\r\nStart a for-loop that iterates over the list. Let's call the iterator i.\r\n\r\nFor each i, initialize two pointers, start and end. start should be initialized to i + 1, and end should be initialized to n - 1.\r\n\r\nWhile start is less than end:\r\n\r\nCalculate the sum of the numbers at indices i, start, and end. Let's call this current_sum.\r\n\r\nIf current_sum is equal to the target, we've found our triplet. Return the numbers at indices i, start, and end.\r\n\r\nIf current_sum is less than the target, increment start by one. This works because our list is sorted, and we know that the next potential number is greater than the current start.\r\n\r\nIf current_sum is greater than the target, decrement end by one. This is because we need to reduce our current sum, so we try the next smallest number from the end.\r\n\r\nIf no triplet is found that sums to the target, return None.\r\n\r\nThe time complexity for this solution is O(n^2), where n is the length of the list. This is because we're using a for-loop that runs n times (O(n)), and within this loop, we could potentially move the start and end pointers across the entire list (O(n)), giving us a total time complexity of O(n^2).\r\n\r\nThe space complexity is O(n) if you consider the space required for the output and O(1) additional space, not considering the input and output size, as we're only using a few variables to keep track of the indices and current sum, which doesn't change with the size of the input list.",
            };
            await LoadSingle(codeProblem);
            //foreach (var d in dummy)
            //{
            //    await LoadSingle(d);
            //}
        }

        private List<CreateCodeProblemRequest> GetDummyProblems()
        {
            var requests = new List<CreateCodeProblemRequest>();

            for (int i = 0; i < 20; i++)
            {
                var complexity = CodeProblemComplexity.Easy;
                if (i % 2 == 0)
                {
                    complexity = CodeProblemComplexity.Medium;
                }
                if (i % 3 == 0)
                {
                    complexity = CodeProblemComplexity.Hard;
                }

                requests.Add(new()
                {
                    Description = "Write a function that takes in a string as input and determines whether it is a palindrome or not. A palindrome is a word, phrase, number, or other sequence of characters that reads the same forward and backward, ignoring spaces, punctuation, and capitalization.",
                    Name = i.ToString() + " some unreal problem for testing",
                    Examples = new()
                {
                    new("racecar", "true"),
                    new("A man, a plan, a canal: Panama", "true"),
                    new("hello", "false"),
                },
                    Constraints = new()
                {
                    "Input string length <= 10^5",
                },
                    ComplexityTypeId = complexity,
                    ParameterNames = new()
                {
                    "str",
                },
                    ParameterTypes = new()
                {
                    InternalType._String,
                },
                    ReturnType = InternalType._Bool,
                    Tags = new()
                {
                    "string",
                    "palindrome",
                },
                    TestCaseSetUUID = Guid.Parse("2957c7bc-a211-432b-95ed-d556d950691c"),
                });
            }

            return requests;
        }

        private async Task LoadSingle(CreateCodeProblemRequest codeProblem)
        {
            try
            {
                Console.WriteLine("Loading {0}.", codeProblem.Name);
                await _codeProblemService.CreateAsync(codeProblem);

                var testCases = _testCasesProvider.GetTestCaseSet(codeProblem.TestCaseSetUUID);
                if (testCases != null && testCases.Count > 0)
                {
                    await _testCaseService.CreateTestCaseSetAsync(testCases.Select(t => new Mongo.Data.Models.TestCaseItem
                    {
                        Id = t.Id,
                        Input = t.Input,
                        Expected = t.Expected,
                    }).ToList(), codeProblem.TestCaseSetUUID);
                }
                Console.WriteLine("{0} loaded.", codeProblem.Name);
            }
            catch
            {
                Console.WriteLine("Failed to load {0}.", codeProblem.Name);
                // Most likely it's already exists or mongo is down so ignore
            }
        }
    }
}