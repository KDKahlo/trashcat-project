using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)] // meaning we are running on 2 parallel devices

//worth rewatching "Parallel Test Execution" video in the AltTester course for set up to AltTester software.
//requires steps similar to initial setup but for 2 devices. 