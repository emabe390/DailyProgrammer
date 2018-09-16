#ran with python 2.7.15
import oeis_tester
import math
import time

test_maximum_values = dict()
is_sequence_dict = dict()

cached_primes = dict()
def isPrime(n):
	if n<2:
		return False
	if n not in cached_primes.keys():
		populatePrimes(n)
	return cached_primes[int(n)]
def populatePrimes(n):
	#print "Populating primes..."
	primes = []
	low = 2
	if len(cached_primes.keys()) > 0:
		for x in cached_primes:
			if cached_primes[x]:
				primes.append(x)

		low = max(primes)+1
	for x in range(low, int(n + 1)):
		#print x
		isPrime = True
		for y in primes:
			if x % y == 0:
				isPrime = False
				break
		if isPrime:
			primes.append(x)
		cached_primes[x]=isPrime

is_sequence_dict["oeis_006125"] = True
def oeis_006125(i):
	return int(2**(i*(i-1)/2))

data_071954 = dict()
data_071954[0] = 2
data_071954[1] = 4
is_sequence_dict["oeis_071954"] = True
def oeis_071954(i):
	if i not in data_071954.keys():
		data_071954[i] = 4*oeis_071954(i-1)-oeis_071954(i-2)-4
	return data_071954[i]

data_002812 = dict()
data_002812[0] = 2
is_sequence_dict["oeis_002812"] = True
def oeis_002812(i):
	if i not in data_002812.keys():
		data_002812[i] = 2*data_002812[i-1]**2-1
	return data_002812[i]
	
	
data_004148 = dict()
data_004148[0] = 1
data_004148[1] = 1
data_004148[2] = 1
is_sequence_dict["oeis_004148"] = True
def oeis_004148(i):
	if i not in data_004148.keys():
		nw = data_004148[i-1]
		for x in range(1, i-2+1):
			nw+=data_004148[x]*data_004148[i-2-x]
		
		data_004148[i] = nw
	return data_004148[i]

def getDivisors(i):
	res = []
	for x in range(1, i+1):
		if i%x==0:
			res.append(x)
	return res

data_002065 = dict()
data_002065[0] = 0
is_sequence_dict["oeis_002065"] = True
def oeis_002065(i):
	if i not in data_002065.keys():
		data_002065[i] = data_002065[i-1]**2+data_002065[i-1]+1
	#print data_002065[i], "\n"
	return data_002065[i]

is_sequence_dict["oeis_069283"] = True
def oeis_069283(i):
	return sum(1 for i in getDivisors(i) if i%2==1)-1

is_sequence_dict["oeis_007521"] = False
def oeis_007521(i):
	return (i-5)%8==0 and isPrime(i)
	
is_sequence_dict["oeis_045762"] = False
test_maximum_values["oeis_045762"] = 15
def oeis_045762(i):
	return not isPrime(math.pow(2,i)-1)

is_sequence_dict["oeis_061812"] = False
def oeis_061812(i):
	"""Returns True if i follows condition: sqrt(floor(Pi*k)) == floor(sqrt(floor(Pi*k)))"""
	return math.sqrt(math.floor(math.pi*i)) == math.floor(math.sqrt(math.floor(math.pi*i)))

is_sequence_dict["oeis_001952"] = True
#test_maximum_values["oeis_001952"] = 18
def oeis_001952(i):
	return math.floor(i*(2 + 2**0.5))

is_sequence_dict["oeis_048724"] = True
#test_maximum_values["oeis_048724"] = 10
def oeis_048724(i):
	s = "{0:b}".format(i)
	s2= "{0:b}".format(2*i)
	s = s.rjust(len(s2),"0")
	x = ""
	for i in range(0, len(s)):
		if s[i]=="0":
			x+=s2[i]
		elif s2[i]=="0":
			x+=s[i]
		else:
			x+="0"
	#print x
	return int(x,2)

def test(specific = "default"):
	#oeis_tester.is_sequence_dict = is_sequence_dict
	_t = time.time()
	oeis_tester.print_errors = True
	oeis_tester.quick_fail = True
	g = globals()
	tests = 0
	failed_tests = []
	for name in g:
		if "oeis_" in name and hasattr(g[name], '__call__'):
			if not(specific == "default") and not(specific == name):
				continue
			is_sequence = is_sequence_dict[name]
			t = time.time()
			res = -1
			if name in test_maximum_values:
				res = oeis_tester.test_against_oeis(name.split("_")[1], g[name], maximum_value=test_maximum_values[name], is_sequence=is_sequence)
			else:
				res = oeis_tester.test_against_oeis(name.split("_")[1], g[name], is_sequence=is_sequence)
			if res is True:
				tests+=1
			else:
				failed_tests.append(name)
			elapsed = time.time() - t
			print "  Test (%s) passed with result %s after %s seconds" % (name, res, str(round(elapsed,2)))
	elapsed = str(round(time.time() - _t,2))
	if not len(failed_tests) == 0:
		print "\nThe following tests failed:"
		for x in failed_tests:
			print x
	print "\n%i/%i tests passed after %s seconds" % (tests, tests + len(failed_tests), elapsed)
		