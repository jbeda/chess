import subprocess
import os, os.path
def run(args: list[str]):
    failed = False
    if subprocess.call(["dotnet", "test"], cwd=os.path.join(os.getcwd(), "src")) > 0 and not failed:
        failed = True
    return int(failed)