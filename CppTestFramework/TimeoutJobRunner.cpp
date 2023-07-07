#include "pch.h"
#include "TimeoutJobRunner.h"

std::chrono::duration<double> TimeoutJobRunner::ElapsedTimeSeconds;
double TimeoutJobRunner::TimeLimitSeconds;
bool TimeoutJobRunner::IsRunning;