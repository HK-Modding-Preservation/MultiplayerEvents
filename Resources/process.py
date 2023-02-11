import csv
import json
import re

def baseName(ret):
    ret = ret.lower();
    ret = ret.replace("(clone)", "");
    ret = ret.strip();
    ret = ret.replace("cln", "");
    ret = ret.strip();
    ret = re.sub( r'\([0-9+]+\)', "",ret);
    ret = ret.strip();
    ret = re.sub( r'[0-9+]+$', "",ret);
    ret = ret.strip();
    ret = ret.replace("(clone)", "");
    ret = ret.strip();
    return ret

f = open('Geo.csv')
csv_reader = csv.reader(f)
enemyToPaths={}
for line in csv_reader:
    ename = line[0].split("/")[-1]
    if line[1] not in enemyToPaths:
        enemyToPaths[line[1]]= set()
        enemyToPaths[line[1]].add(baseName(ename))
    else:
        enemyToPaths[line[1]].add(baseName(ename))
f.close()

for enames in enemyToPaths.keys():
    enemyToPaths[enames] = list(enemyToPaths[enames])


with open("out.json", "w") as outfile:
    json.dump(enemyToPaths, outfile, indent=4, sort_keys=True)

