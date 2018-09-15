#ran with python2.7.15
import sys
import requests
import math
print_errors = False
quick_fail = False

def find_print_points(min, max, number):
	return [math.floor(min + (max-min)/float(number) * k) for k in range(0,number)]

def test_against_oeis(call_id, function="default", minimum_value="default", maximum_value="default", print_percentage=False, number_of_prints=20):
	seq = call_oeis(call_id)
	data = seq.values()
	data.sort()
	passing = True
	
	function_name = ""
	
	if function == "default":
		function_name = "oeis_" + call_id
		function=globals()[function_name]
	
	function_name = function.__name__
	is_sequence = is_sequence_dict[function_name]
	if minimum_value == "default":
		minimum_value = 1
	if maximum_value == "default":
		if is_sequence is True:
			maximum_value = max(seq.keys())
		else:
			maximum_value = max(data)
	print_points = find_print_points(minimum_value, maximum_value, number_of_prints)
	
	p("%s testing between %i and %i" % (function_name, minimum_value, maximum_value), force=True)
	for x in range(minimum_value, maximum_value+1):
		if len(data)==0:
			p("Maximum value outside of data (%i)", x, force=True)
			break
		if print_percentage and x in print_points:
			print_points.remove(x)
			p((number_of_prints-len(print_points))*(100/number_of_prints), "% (", x, ")", force=True)
		res = function(x)
		if is_sequence is True:
			#the data is a sequence!
			if x not in seq.keys():
				p(x, force=True)
			if seq[x] != res:
					if quick_fail is True:
						return False
					passing = False
					p("%s: Error at %i, expected %i, got %i" % (function_name, x, seq[x], res))
		else:
			if res is False:
				if x == data[0] or (x>data[0] and x in data):
					if quick_fail is True:
						return False
					passing = False
					p("%s: Error at %i, expected True, got False" % (function_name, x))
						
			else:
				if not x == data[0]:
					if x in data:
						data.remove(x)
					else:
						if quick_fail is True:
							return False
						passing = False
						p("%s: Error at %i, expected False, got True" % (function_name, x))
				else:
					data.pop(0)
	if (maximum_value is "default" and minimum_value is "default") and len(data) is not 0:
		p(" Not enought values tried in %s, still %i left" % (function_name, len(data)), force=True)
	return passing

def p(x, force=False):
	if force is True or print_errors is True:
		print x

def call_oeis(id_):
	r = requests.get('http://oeis.org/A%s/b%s.txt' % (id_, id_))
	inx = [x.split() for x in r.content.decode('UTF-8').splitlines()]
	try:
		s = [[int(x) for x in y] for y in inx if len(y) == 2]	
		D = {x:y for x,y in s}
		return D
	except ValueError:
		p("Unknown OEIS-sequence \"%s\"" % id_,force=True)
		sys.exit(-1)

if __name__ == "__main__":
	import oeis_tests
	if len(sys.argv) is 1:
		oeis_tests.test()
	else:
		for x in range (1, len(sys.argv)):
			oeis_tests.test(sys.argv[x])