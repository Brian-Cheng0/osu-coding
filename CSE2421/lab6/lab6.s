.file "sum.s"

.section	.rodata.str1.1,"aMS",@progbits,1
PR_1:
	.string "\nSize of the array: %i\n"
Result:
	.string "\nThe sum is: %i"
	
PInt:
	.string "%i "
	
.data
.align 8 
.string "This is the sum: %i"
array:
	.quad 0
	.quad 0
	.quad 0
	.quad 0
	.quad 0
	.quad 0
	.quad 0
	.quad 0
	.quad 0
	.quad 0

.globl sum
	.type	sum, @function
.text
sum:
	pushq %rbp		# stack frame management
	movq %rsp, %rbp	# stack frame management
	
	pushq %rsi
	pushq %rdi

# do the sum        
# in some register r8
#load string  address to rdi
#load sum value into rsi
#move 0 to rax
#call printf
	movq $PR_1, %rdi	#pass the count to the rdi
	movq $0,%rax		
	call printf
	popq %rdi		#rdi pop from stack
	popq %rsi		#rsi pop from stack 
     loop:
	decq %rsi		#rsi decrement -1
	jl exit			#jump to exit
	addq (%rdi,%rsi,8), %r8	#add number to r8
      jmp loop

exit:
	movq $0, %rax
	 movq %r8, %rsi		#give the number to rsi
	 movq $Result, %rdi	
	call printf	
	leave
	ret
.size sum,.-sum
