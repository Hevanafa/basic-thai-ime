# utility script to clean empty lines from a tab-separated value file

# frequency pair: word, count
pairs = []

File.readlines("frequency_list.txt").each do |line|
	pairs += [line.chomp.split("\t")] if line[0] != "\t"
end

File.open("frequency_list_clean.txt", "w") do |file|
	file.write(pairs
			.map{ |line| line.join "\t" }
			.join "\n")
end