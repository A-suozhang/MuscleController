
def saveAndClassifySeq(l, seqMat):
    if (l[0] < acitivateTreshold or l[1] < acitivateTreshold):
	        return seqMat
    if (l[0] > acitivateTreshold):
        seqMat[0].append(l[0])
    if (l[1] > acitivateTreshold):
        seqMat[1].append(l[1])
    
    return seqMat

seqMat = [[],[]]
acitivateTreshold = 5
Mat = [[1,6],[10,10], [20,25], [30,35], [1,40]]
for i in Mat:
	# print (i)
	seqMat = saveAndClassifySeq(i, seqMat)
	print (seqMat)


