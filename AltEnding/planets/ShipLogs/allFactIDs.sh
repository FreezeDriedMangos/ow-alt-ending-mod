files="*.xml"
regex="[0-9]+_([a-z]+)_[0-9a-z]*"
for f in $files    # unquoted in order to allow the glob to expand
do
	cat $f | tr -d '\n' | grep '\<ID\>.+\</ID\>'
    # if [[ $f =~ $regex ]]
    # then
    #     name="${BASH_REMATCH[1]}"
    #     echo "${name}.jpg"    # concatenate strings
    #     name="${name}.jpg"    # same thing stored in a variable
    # else
    #     echo "$f doesn't match" >&2 # this could get noisy if there are a lot of non-matching files
    # fi
done